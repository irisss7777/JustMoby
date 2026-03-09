using UnityEngine;

namespace Contracts.PresentationContracts.Signals.View.CubeAnimation
{
    public struct CubeSetPositionSignal
    {
        public Vector2 Position { get; private set; }
        public int ViewId { get; private set; }

        public CubeSetPositionSignal(Vector2 position, int viewId)
        {
            Position = position;
            ViewId = viewId;
        }
    }
}