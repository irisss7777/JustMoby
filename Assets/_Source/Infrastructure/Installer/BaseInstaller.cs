using Infrastructure.Factory.Cube;
using Zenject;

namespace Infrastructure.Installer
{
    public class BaseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            FactoryBind();
        }

        private void FactoryBind()
        {
            Container.BindInterfacesAndSelfTo<CubeFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<CubeUiFactory>().AsSingle();
        }
    }
}