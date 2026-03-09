using System;
using Contracts.ApplicationContracts.Signal;
using Contracts.PresentationContracts.View;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Presentation.View.Cube
{
    public class CubeView : MonoBehaviour, ICubeView, IPointerDownHandler
    {
        public event Action OnDispose;
        public event Action MouseButtonUp;
        public Transform Transform => transform;

        [SerializeField] private Image _image;
        private Canvas _mainCanvas;

        private bool _isDragging = true;
        private bool _canDraggable = true;
        
        public void Initialize()
        {
            _mainCanvas = GetComponentInParent<Canvas>();
        }

        public void SetMaskable(bool isMaskable) =>
            _image.maskable = isMaskable;
        
        public void SetSprite(Sprite sprite) =>
            _image.sprite = sprite;

        public void SetDragging(bool isDraggging)
        {
            _isDragging = isDraggging;

            if (!isDraggging && _canDraggable)
                OnMouseButtonUp();
        }

        private void Update()
        {
            if (_isDragging && _canDraggable)
                MoveWithMouse();
        }

        private void MoveWithMouse() =>
            _image.rectTransform.localPosition = GetLocalPointerPosition();

        private Vector2 GetLocalPointerPosition()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _mainCanvas.transform as RectTransform, 
                Input.mousePosition, 
                _mainCanvas.worldCamera, 
                out Vector2 localPoint);

            return localPoint;
        }

        private void OnMouseButtonUp()
        {
            _image.raycastTarget = true;
            
            _canDraggable = false;
            
            MouseButtonUp?.Invoke();
        }

        public void Dispose()
        {
            OnDispose?.Invoke();
            _image.DOFade(0, 1f).OnComplete(() => Destroy(gameObject));
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _canDraggable = true;
            _image.raycastTarget = false;
        }
    }
}