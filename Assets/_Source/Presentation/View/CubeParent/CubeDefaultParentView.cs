using Contracts.PresentationContracts.View;
using UnityEngine;

namespace Presentation.View.CubeParent
{
    public class CubeDefaultParentView : MonoBehaviour, ICubeDefaultParentView
    {
        public Transform Transform => _parent;

        [SerializeField] private Transform _parent;
    }
}