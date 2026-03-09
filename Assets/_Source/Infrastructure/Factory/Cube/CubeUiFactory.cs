using Contracts.PresentationContracts.Database.Repos;
using Contracts.PresentationContracts.View;
using Presentation.View.Cube;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factory.Cube
{
    public class CubeUiFactory
    {
        [Inject] private readonly ICubeDatabase _cubeDatabase;
        [Inject] private readonly ICubeUiGridView _cubeUiGridView;
        [Inject] private readonly MessageBus _messageBus;

        public void CreateCubeUi()
        {
            for (var i = 0; i < _cubeDatabase.CubeCounts; i++)
                CreateObject(i);
        }

        private void CreateObject(int cubeId)
        {
            var prefab = _cubeDatabase.CubeUiPrefab as CubeUiView;

            var cubeUi = Object.Instantiate(prefab, _cubeUiGridView.ContentHandler);

            cubeUi.Initialize(_messageBus, cubeId, _cubeDatabase.GetSpriteById(cubeId), _cubeDatabase.DragDelay);
        }
    }
}