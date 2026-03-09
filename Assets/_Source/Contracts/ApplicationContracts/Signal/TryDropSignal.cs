using UnityEngine;

namespace Contracts.ApplicationContracts.Signal
{
    public struct TryDropSignal
    {
        public int ViewId { get; private set; }
        public int CubeId { get; private set; }
        public Vector2 Position { get; private set; }

        public TryDropSignal(int viewId, int cubeId, Vector2 position)
        {
            ViewId = viewId;
            CubeId = cubeId;
            Position = position;
        }
    }
}