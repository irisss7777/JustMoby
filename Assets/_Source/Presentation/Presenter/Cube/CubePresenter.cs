using System;
using Contracts.ApplicationContracts.Signal;
using Contracts.PresentationContracts.Database.Config.Animation;
using Contracts.PresentationContracts.Signals.View;
using Contracts.PresentationContracts.Signals.View.CubeAnimation;
using Contracts.PresentationContracts.View;
using DG.Tweening;
using Plugins.MessagePipe.MessageBus.Runtime;
using Presentation.View.Cube;
using UnityEngine;

namespace Presentation.Presenter.Cube
{
    public class CubePresenter : IMessageDisposable
    {
        public event Action OnDispose;
        
        private readonly ICubeView _cubeView;
        private readonly ICubeDropDownConfig _cubeDropDownConfig;
        private readonly ICubeHoleParentView _holeParentView;
        private readonly MessageBus _messageBus;

        private readonly int _viewId;
        private readonly int _cubeId;
        
        public CubePresenter(ICubeView cubeView,
            MessageBus messageBus,
            int cubeId,
            int viewId,
            Sprite sprite,
            ICubeDropDownConfig cubeDropDownConfig,
            ICubeHoleParentView holeParentView)
        {
            _cubeView = cubeView;
            _messageBus = messageBus;
            _cubeId = cubeId;
            _viewId = viewId;
            _cubeDropDownConfig = cubeDropDownConfig;
            _holeParentView = holeParentView;

            _cubeView.SetSprite(sprite);
            
            this.Subscribe(_messageBus, (ClickInputSignal signal) => _cubeView.SetDragging(signal.ClickState));
            this.Subscribe(_messageBus, (DestroyCubeSignal signal) => Destroy(signal.ViewId));
            this.Subscribe(_messageBus, (CubeDropDownSignal signal) => CubeDropDown(signal.TargetPosition, signal.ViewId, signal.Type));
            this.Subscribe(_messageBus, (CubeSetPositionSignal signal) => SetPosition(signal.Position, signal.ViewId));
            this.Subscribe(_messageBus, (DestroyInHoleSignal signal) => DropToHole(signal.ViewId));

            _messageBus.Publish(new SetCubeGridStateSignal(false));
            
            _cubeView.MouseButtonUp += MouseButtonUp;
        }

        private void CubeDropDown(Vector2 targetPosition, int viewId, DropDownType type)
        {
            if (_viewId != viewId) 
                return;

            switch (type)
            {
                case DropDownType.Jump:
                    CubeDropDownJump(targetPosition);
                    break;
                case DropDownType.Fall:
                    CubeDropDownFall(targetPosition);
                    break;
            }
        }
        
        private void CubeDropDownJump(Vector2 targetPosition)
        {
            Vector2 startPosition = _cubeView.Transform.position;
            Vector2 peakPosition = new Vector2((startPosition.x + targetPosition.x) * 0.5f, startPosition.y + _cubeDropDownConfig.JumpHoleHeight);

            DOTween.To(() => 0f, p =>
            {
                float u = 1f - p;
                Vector2 position = u * u * startPosition + 2f * u * p * peakPosition + p * p * targetPosition;
                
                _cubeView.Transform.position = position;
            }, 1f, _cubeDropDownConfig.JumpDuration).SetEase(Ease.OutQuad);
        }
        
        private void CubeDropDownFall(Vector2 targetPosition)
        {
            _cubeView.Transform.DOMove(targetPosition, 0.5f);
        }

        private void SetPosition(Vector2 position, int viewId)
        {
            if (_viewId != viewId) 
                return;

            _cubeView.Transform.position = position;
        }

        private void DropToHole(int viewId)
        {
            if (_viewId != viewId) 
                return;
            
            _cubeView.SetMaskable(true);
            _cubeView.Transform.SetParent(_holeParentView.Parent);
    
            Vector2 startPosition = _cubeView.Transform.position;
            Vector2 targetPosition = _holeParentView.Position + Vector2.down * 5f;
            Vector2 peakPosition = new Vector2((startPosition.x + _holeParentView.Position.x) * 0.5f, startPosition.y + _cubeDropDownConfig.JumpHeight);

            DOTween.To(() => 0f, p =>
            {
                float u = 1f - p;
                Vector2 pos = u * u * startPosition + 2f * u * p * peakPosition + p * p * targetPosition;
                _cubeView.Transform.position = pos;
            }, 1f, _cubeDropDownConfig.JumpDuration).SetEase(Ease.OutQuad).OnComplete(() => Destroy(viewId));
        }

        private void MouseButtonUp()
        {
            _messageBus.Publish(new SetCubeGridStateSignal(true));
            _messageBus.Publish(new TryDropSignal(_viewId, _cubeId, _cubeView.Transform.position));
        }

        private void Destroy(int viewId)
        {
            if(viewId != _viewId)
                return;

            Dispose();
        }

        public void Dispose()
        {
            OnDispose?.Invoke();
            
            _cubeView.MouseButtonUp -= MouseButtonUp;
            
            _cubeView.Dispose();
        }
    }
}