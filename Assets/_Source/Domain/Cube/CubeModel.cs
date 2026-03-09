using UnityEngine;

namespace Domain.Cube
{
    public class CubeModel
    {
        public int CubeId => _cubeId;
        public int ViewId => _viewId;
        public Vector2 Position => _position;
        
        private readonly int _cubeId;
        private readonly int _viewId;
        private Vector2 _position;

        public CubeModel(int cubeId, int viewId, Vector2 position)
        {
            _cubeId = cubeId;
            _viewId = viewId;
            _position = position;
        }
    }
}