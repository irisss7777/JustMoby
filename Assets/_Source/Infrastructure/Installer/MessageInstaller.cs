using Contracts.ApplicationContracts.Signal;
using Contracts.PresentationContracts.Signals.View;
using Contracts.PresentationContracts.Signals.View.CubeAnimation;
using MessagePipe;
using Plugins.MessagePipe.MessageBus.Runtime;
using Zenject;

namespace Infrastructure.Installer
{
    public class MessageInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var options = Container.BindMessagePipe();

            Container.Bind<MessageBus>().AsSingle();

            InputBind(options);
            FactoryBind(options);
            PresentationBind(options);
            ApplicationBind(options);
        }

        private void InputBind(MessagePipeOptions options)
        { 
            Container.BindMessageBroker<ClickInputSignal>(options);
        }
        
        private void FactoryBind(MessagePipeOptions options)
        { 
            Container.BindMessageBroker<CubeUiCreateSignal>(options);
        }

        private void PresentationBind(MessagePipeOptions options)
        { 
            Container.BindMessageBroker<SetCubeGridStateSignal>(options);
            Container.BindMessageBroker<DestroyCubeSignal>(options);
            Container.BindMessageBroker<CubeSetPositionSignal>(options);
            Container.BindMessageBroker<CubeDropDownSignal>(options);
            Container.BindMessageBroker<HoleStateSignal>(options);
            Container.BindMessageBroker<DestroyInHoleSignal>(options);
        }

        private void ApplicationBind(MessagePipeOptions options)
        {
            Container.BindMessageBroker<TryDropSignal>(options);
            Container.BindMessageBroker<PointerInRightPanelSignal>(options);
        }
    }
}