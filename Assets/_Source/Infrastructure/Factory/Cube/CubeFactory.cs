using System;
using System.Collections.Generic;
using Application.Cube;
using Application.Cube.CanDropValidation;
using Contracts.PresentationContracts.Database.Config.Animation;
using Contracts.PresentationContracts.Database.Repos;
using Contracts.PresentationContracts.Signals.View;
using Contracts.PresentationContracts.View;
using Presentation.View.Cube;
using Plugins.MessagePipe.MessageBus.Runtime;
using Presentation.Presenter.Cube;
using UnityEngine;
using Utils;
using Zenject;
using Object = UnityEngine.Object;

namespace Infrastructure.Factory.Cube
{
    public class CubeFactory : IInitializable, IMessageDisposable
    {
        public event Action OnDispose;
        
        [Inject] private readonly ICubeDatabase _cubeDatabase;
        [Inject] private readonly ICubeDefaultParentView _cubeDefaultParentView;
        [Inject] private readonly MessageBus _messageBus;
        [Inject] private readonly ICubeDownCornerView _cornerView;
        [Inject] private readonly ICubeDropDownConfig _cubeDropDownConfig;
        [Inject] private readonly ICubeHoleParentView _holeParentView;
        

        public void Initialize()
        {
            CreateService();
            
            this.Subscribe(_messageBus, (CubeUiCreateSignal signal) => CreateView(signal.CubeId, signal.Position));
        }

        private void CreateView(int cubeId, Vector2 position)
        {
            var prefab = _cubeDatabase.CubePrefab as CubeView;

            var view = Object.Instantiate(prefab, _cubeDefaultParentView.Transform);
            
            view.Initialize();

            view.transform.position = position;

            CreateService(view, cubeId);
        }
        
        private void CreateService(CubeView view, int cubeId) => 
            new CubePresenter(view,
                _messageBus,
                cubeId,
                UniqueIdService.CreateId<CubeView>(),
                _cubeDatabase.GetSpriteById(cubeId),
                _cubeDropDownConfig,
                _holeParentView);

        private void CreateService()
        {
            var collectionsService = new CubeCollectionsService(_messageBus);
            var cubeDrop = new CubeDropService(_messageBus,
                collectionsService,
                CreateCanDropValidServices(),
                _cornerView,
                _cubeDatabase.DistanceBetweenCube,
                _cubeDatabase.VerticalDistanceBetweenCube);
        }

        private List<ACanDropValidService> CreateCanDropValidServices()
        {
            List<ACanDropValidService> services = new();
            
            services.Add(new DistanceCanDropService(_messageBus, _cubeDatabase.DistanceBetweenCube, _cubeDatabase.VerticalDistanceBetweenCube));
            services.Add(new RightPanelDropValidService(_messageBus));

            return services;
        }

        public void Dispose()
        {
            OnDispose?.Invoke();
        }
    }
}