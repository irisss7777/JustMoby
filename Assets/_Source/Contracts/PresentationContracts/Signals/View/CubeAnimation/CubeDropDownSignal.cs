using Contracts.PresentationContracts.Database.Config.Animation;
using UnityEngine;

namespace Contracts.PresentationContracts.Signals.View.CubeAnimation
{
    public struct CubeDropDownSignal
    {
        public Vector2 TargetPosition { get; private set; }
        public int ViewId { get; private set; }
        public DropDownType Type { get; private set; }

        public CubeDropDownSignal(Vector2 targetPosition, int viewId, DropDownType type)
        {
            TargetPosition = targetPosition;
            ViewId = viewId;
            Type = type;
        }
    }
}