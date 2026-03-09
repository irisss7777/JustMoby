using Contracts.PresentationContracts.View;
using UnityEngine;

namespace Presentation.View.CubeParent
{
    public class CubeDownCornerView : MonoBehaviour, ICubeDownCornerView
    {
        public Transform DownCorner => transform;
        public Transform RightCorner => _rightCorner;
        public Transform LeftCorner => _leftCorner;

        [SerializeField] private Transform _rightCorner;
        [SerializeField] private Transform _leftCorner;
    }
}