using System;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;

namespace Contracts.PresentationContracts.View
{
    public interface ICubeView : IMessageDisposable
    {
        public event Action MouseButtonUp;
        public Transform Transform { get; }
        public void Initialize();
        public void SetMaskable(bool isMaskable);
        public void SetSprite(Sprite sprite);
        public void SetDragging(bool isDragging);
    }
}