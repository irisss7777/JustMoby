using Contracts.PresentationContracts.View;
using UnityEngine;

namespace Contracts.PresentationContracts.Database.Repos
{
    public interface ICubeDatabase
    {
        public int CubeCounts { get; }
        public ICubeUiView CubeUiPrefab { get; }
        public ICubeView CubePrefab { get; }
        public float DragDelay { get; }
        public float DistanceBetweenCube { get; }
        public float VerticalDistanceBetweenCube { get; }

        public Sprite GetSpriteById(int id);
    }
}