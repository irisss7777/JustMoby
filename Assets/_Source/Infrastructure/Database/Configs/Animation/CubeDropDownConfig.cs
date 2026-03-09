using Contracts.PresentationContracts.Database.Config.Animation;
using UnityEngine;

namespace Infrastructure.Database.Configs.Animation
{
    [CreateAssetMenu(fileName = "CubeDropDownConfig", menuName = "Scriptable/Config/CubeDropDownConfig")]
    public class CubeDropDownConfig : ScriptableObject, ICubeDropDownConfig
    {
        public float JumpHeight => _jumpHeight;
        public float JumpHoleHeight => _jumpHoleHeight;
        public float JumpDuration => _jumpDuration;
        
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _jumpHoleHeight;
        [SerializeField] private float _jumpDuration;
    }
}