using Contracts.ApplicationContracts.Signal;
using Cysharp.Threading.Tasks;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Presentation.View.Panels
{
    public class RightPanelView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Inject] private readonly MessageBus _messageBus;
        
        public void OnPointerEnter(PointerEventData eventData) =>
            _messageBus.Publish(new PointerInRightPanelSignal(true));

        public void OnPointerExit(PointerEventData eventData) =>
            _messageBus.Publish(new PointerInRightPanelSignal(false));
    }
}