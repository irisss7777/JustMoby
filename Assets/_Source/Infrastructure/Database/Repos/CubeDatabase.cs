using Contracts.PresentationContracts.Database.Repos;
using Contracts.PresentationContracts.View;
using Infrastructure.Database.Configs;
using Presentation.View.Cube;
using UnityEngine;

namespace Infrastructure.Database.Repos
{
    [CreateAssetMenu(fileName = "CubeDatabase", menuName = "Scriptable/Database/CubeDatabase")]
    public class CubeDatabase : ScriptableObject, ICubeDatabase
    {
        [SerializeField] private CubeUiView _cubeUiPrefab;
        [SerializeField] private CubeView _cubePrefab;
        [SerializeField] private float _dragDelay;
        [SerializeField] private float _distanceBetweenCube;
        [SerializeField] private float _verticalDistanceBetweenCube;
        [SerializeField] private CubeConfig[] _cubeConfigs;

        public ICubeUiView CubeUiPrefab => _cubeUiPrefab;
        public ICubeView CubePrefab => _cubePrefab;
        public float DragDelay => _dragDelay;
        public float DistanceBetweenCube => _distanceBetweenCube;
        public float VerticalDistanceBetweenCube => _verticalDistanceBetweenCube;
        public int CubeCounts => _cubeConfigs.Length;

        public Sprite GetSpriteById(int id)
        {
            return _cubeConfigs[id].CubeSprite;
        }
    }
}