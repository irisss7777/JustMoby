using UnityEngine;
using UnityEngine.EventSystems;

namespace Contracts.PresentationContracts.View
{
    public interface ICubeHoleParentView : IPointerEnterHandler, IPointerExitHandler
    {
        public Transform Parent { get; }
        public Vector2 Position { get; }
    }
}