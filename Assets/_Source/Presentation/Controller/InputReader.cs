using Contracts.ApplicationContracts.Signal;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Presentation.Controller
{
    public class InputReader: MonoBehaviour
    {
        [Inject] private readonly MessageBus _messageBus;
        private InputSystem _inputSystem;

        private void OnEnable()
        {
            _inputSystem = new InputSystem();
            _inputSystem.Enable();

            Subscribe();
        }

        private void Subscribe()
        {
            _inputSystem.UI.Click.performed += RunInput;
            _inputSystem.UI.Click.canceled += RunInput;
        }

        private void Unsubscribe()
        {
            _inputSystem.UI.Click.performed -= RunInput;
            _inputSystem.UI.Click.canceled -= RunInput;
        }
        private void RunInput(InputAction.CallbackContext context) =>
            _messageBus.Publish(new ClickInputSignal(context.ReadValueAsButton()));
        
        private void OnDisable()
        {
            _inputSystem.Disable();

            Unsubscribe();
        }
    }
}