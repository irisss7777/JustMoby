using Contracts.PresentationContracts.Database.Config.Animation;
using Contracts.PresentationContracts.Database.Repos;
using Infrastructure.Database.Configs.Animation;
using Infrastructure.Database.Repos;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installer
{
    [CreateAssetMenu(fileName = "ScriptableInstaller", menuName = "Installers/ScriptableInstaller")]
    public class ScriptableInstaller : ScriptableObjectInstaller<ScriptableInstaller>
    {
        [SerializeField] private CubeDatabase _cubeDatabase;
        [Header("Animations configs")]
        [SerializeField] private CubeDropDownConfig _cubeDropDownConfig;

        public override void InstallBindings()
        {
            DatabasesBind();
            AnimationConfigsBind();
        }

        private void DatabasesBind()
        {
            Container.Bind<ICubeDatabase>().FromInstance(_cubeDatabase).AsSingle();
        }

        private void AnimationConfigsBind()
        {
            Container.Bind<ICubeDropDownConfig>().FromInstance(_cubeDropDownConfig).AsSingle();
        }
    }
}