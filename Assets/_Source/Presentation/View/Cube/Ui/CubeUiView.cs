using System;
using System.Threading;
using Contracts.PresentationContracts.Signals.View;
using Contracts.PresentationContracts.View;
using Cysharp.Threading.Tasks;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Presentation.View.Cube
{
    public class CubeUiView : MonoBehaviour, IMessageDisposable, ICubeUiView, IPointerDownHandler, IPointerUpHandler
    {
        public event Action OnDispose;
        
        [SerializeField] private Image _image;
        
        private CancellationTokenSource _tokenSource; 
        private MessageBus _messageBus;
        private int _cubeId;
        private float _dragDelay;

        public void Initialize(MessageBus messageBus, int cubeId, Sprite sprite, float dragDelay)
        {
            _messageBus = messageBus;
            _cubeId = cubeId;
            _image.sprite = sprite;
            _dragDelay = dragDelay;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _tokenSource = new CancellationTokenSource();
            WaitForClick().Forget();
        }

        public void OnPointerUp(PointerEventData eventData) =>
            _tokenSource.Cancel();

        private async UniTask WaitForClick()
        {
            float currentTime = 0f;

            while (currentTime < _dragDelay)
            {
                if(_tokenSource.Token.IsCancellationRequested)
                    return;
                
                currentTime += Time.fixedDeltaTime;

                await UniTask.Yield();
            }

            OnStartDrag();
        }

        private void OnStartDrag() =>
            _messageBus.Publish(new CubeUiCreateSignal(_cubeId, transform.position));

        public void Dispose()
        {
            OnDispose?.Invoke();
            _tokenSource.Cancel();
        }
    }
}