using UnityEngine;

namespace Contracts.PresentationContracts.View
{
    public interface ICubeDownCornerView
    {
        public Transform DownCorner { get; }
        public Transform RightCorner { get; }
        public Transform LeftCorner { get; }
    }
}