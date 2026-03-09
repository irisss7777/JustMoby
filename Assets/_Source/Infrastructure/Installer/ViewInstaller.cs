using Contracts.PresentationContracts.View;
using Presentation.View.CubeParent;
using Presentation.View.Ui;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installer
{
    public class ViewInstaller : MonoInstaller
    {
        [SerializeField] private CubeUiGridView _cubeUiGridView;
        [SerializeField] private CubeDefaultParentView _cubeDefaultParentView;
        [SerializeField] private CubeHoleParentView _cubeHoleParentView;
        [SerializeField] private CubeDownCornerView _cubeDownCornerView;

        public override void InstallBindings()
        {
            Container.Bind<ICubeUiGridView>().FromInstance(_cubeUiGridView).AsSingle();
            Container.Bind<ICubeDefaultParentView>().FromInstance(_cubeDefaultParentView).AsSingle();
            Container.Bind<ICubeHoleParentView>().FromInstance(_cubeHoleParentView).AsSingle();
            Container.Bind<ICubeDownCornerView>().FromInstance(_cubeDownCornerView).AsSingle();
        }
    }
}