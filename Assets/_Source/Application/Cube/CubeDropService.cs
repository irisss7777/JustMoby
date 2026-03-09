using System;
using System.Collections.Generic;
using Application.Cube.CanDropValidation;
using Contracts.ApplicationContracts.Signal;
using Contracts.PresentationContracts.Database.Config.Animation;
using Contracts.PresentationContracts.Signals.View;
using Contracts.PresentationContracts.View;
using Contracts.PresentationContracts.Signals.View.CubeAnimation;
using Cysharp.Threading.Tasks;
using Domain.Cube;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Application.Cube
{
    public class CubeDropService : AService
    {
        private readonly ICubeDownCornerView _cornerView;
        private readonly CubeCollectionsService _collectionsService;
        private readonly List<ACanDropValidService> _canDropValidServices;
        private readonly float _distanceBetweenCube;
        private readonly float _verticalDistanceBetweenCube;

        private bool _isInHole;
        
        public CubeDropService(MessageBus messageBus, 
            CubeCollectionsService collectionsService, 
            List<ACanDropValidService> canDropValidServices, 
            ICubeDownCornerView cornerView, 
            float distanceBetweenCube, float verticalDistanceBetweenCube) : base(messageBus)
        {
            _collectionsService = collectionsService;
            _canDropValidServices = canDropValidServices;
            _cornerView = cornerView;
            _distanceBetweenCube = distanceBetweenCube;
            _verticalDistanceBetweenCube = verticalDistanceBetweenCube;

            this.Subscribe(MessageBus, (TryDropSignal signal) => TryDrop(signal.ViewId, signal.CubeId, signal.Position));
            this.Subscribe(MessageBus, (HoleStateSignal signal) => SetInHole(signal.InHole));
        }

        private void SetInHole(bool inHole) =>
            _isInHole = inHole;

        private void TryDrop(int viewId, int cubeId, Vector2 position, bool isOther = false)
        {
            List<CubeModel> tower = _collectionsService.GetTower();

            if (_isInHole)
            {
                MessageBus.Publish(new DestroyInHoleSignal(viewId));
                var removedIndex = _collectionsService.RemoveCubeFromTower(viewId);
                if(removedIndex >= 0)
                    TryDropOther(removedIndex).Forget();
                return;
            }

            if (HasItInTower(viewId, tower))
            {
                MessageBus.Publish(new CubeSetPositionSignal(tower.Find(x => x.ViewId == viewId).Position, viewId));
                return;
            }

            foreach (var canDropService in _canDropValidServices)
            {
                if(!canDropService.CanDrop(viewId, cubeId, position, tower, isOther))
                {
                    MessageBus.Publish(new DestroyCubeSignal(viewId));
                    return;
                }
            }
            
            AddCubeToTower(viewId, cubeId, position, tower, !isOther);
        }

        private void AddCubeToTower(int viewId, int cubeId, Vector2 position, List<CubeModel> tower, bool randomize)
        {
            Vector2 targetPosition = _cornerView.DownCorner.position;

            float randomPosition = randomize ? Random.Range(-_distanceBetweenCube, _distanceBetweenCube) : 0f;

            if(tower.Count == 0)
            {
                targetPosition.y = _cornerView.DownCorner.position.y;
                targetPosition.x = Math.Clamp(position.x + randomPosition,  _cornerView.LeftCorner.position.x, _cornerView.RightCorner.position.x);
            }
            else
            {
                targetPosition.y = tower[^1].Position.y + _verticalDistanceBetweenCube;
                var startPosition = randomize ? tower[^1].Position.x : position.x;
                targetPosition.x = Math.Clamp(startPosition + randomPosition, _cornerView.LeftCorner.position.x, _cornerView.RightCorner.position.x);
            }

            MessageBus.Publish(new CubeDropDownSignal(targetPosition, viewId, randomize ? DropDownType.Jump : DropDownType.Fall));

            _collectionsService.AddCubeToTower(viewId, cubeId, targetPosition);
        }

        private async UniTask TryDropOther(int startIndex)
        {
            List<CubeModel> tower = _collectionsService.GetTower();
            List<CubeModel> oldTower = new List<CubeModel>();

            for (int i = startIndex; i < tower.Count; i++)
            {
                oldTower.Add(tower[i]);
            }

            for (int i = oldTower.Count - 1; i >= 0; i--)
            {
                _collectionsService.RemoveCubeFromTower(oldTower[i].ViewId);
            }
            
            await UniTask.Delay(50);
            
            _isInHole = false;

            for (int i = 0; i < oldTower.Count; i++)
            {
                TryDrop(oldTower[i].ViewId, oldTower[i].CubeId, oldTower[i].Position, true);
                await UniTask.Delay(50);
            }
        }

        private bool HasItInTower(int viewId, List<CubeModel> tower)
        {
            foreach (var cube in tower)
            {
                if (cube.ViewId == viewId)
                    return true;
            }

            return false;
        }
    }
}