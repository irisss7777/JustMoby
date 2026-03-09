using UnityEngine;

namespace Contracts.PresentationContracts.Signals.View
{
    public struct CubeUiCreateSignal
    {
        public int CubeId { get; private set; }
        public Vector2 Position { get; private set; }

        public CubeUiCreateSignal(int cubeId, Vector2 position)
        {
            CubeId = cubeId;
            Position = position;
        }
    }
}