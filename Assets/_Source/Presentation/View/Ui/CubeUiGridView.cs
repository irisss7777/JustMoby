using System;
using Contracts.ApplicationContracts.Signal;
using Contracts.PresentationContracts.View;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Presentation.View.Ui
{
    public class CubeUiGridView : MonoBehaviour, ICubeUiGridView, IMessageDisposable
    {
        public event Action OnDispose;
        public Transform ContentHandler => _contentHandler;

        [Inject] private readonly MessageBus _messageBus;

        [SerializeField] private Transform _contentHandler;
        [SerializeField] private ScrollRect _scrollRect;
        
        private void Awake()
        {
            this.Subscribe(_messageBus, (SetCubeGridStateSignal signal) => SetGridState(signal.IsActive));
        }

        private void SetGridState(bool isActive) =>
            _scrollRect.horizontal = isActive;

        public void Dispose()
        {
            OnDispose?.Invoke();
        }
    }
}