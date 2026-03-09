using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;

namespace Contracts.PresentationContracts.View
{
    public interface ICubeUiView
    {
        public void Initialize(MessageBus messageBus, int cubeId, Sprite sprite, float dragDelay);
    }
}