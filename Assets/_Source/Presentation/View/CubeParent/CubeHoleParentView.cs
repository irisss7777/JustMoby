using Contracts.PresentationContracts.Signals.View;
using Contracts.PresentationContracts.View;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Presentation.View.CubeParent
{
    public class CubeHoleParentView : MonoBehaviour, ICubeHoleParentView
    {
        public Transform Parent => _parent;
        public Vector2 Position => transform.position;

        [Inject] private readonly MessageBus _messageBus;

        [SerializeField] private Transform _parent;
        
        public void OnPointerEnter(PointerEventData eventData) =>
            _messageBus.Publish(new HoleStateSignal(true));

        public void OnPointerExit(PointerEventData eventData) =>
            _messageBus.Publish(new HoleStateSignal(false));
    }
}