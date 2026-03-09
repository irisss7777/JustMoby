using System.Collections.Generic;
using Domain.Cube;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;

namespace Application.Cube
{
    public class CubeCollectionsService : AService
    {
        private readonly List<CubeModel> _tower = new();
        
        public CubeCollectionsService(MessageBus messageBus) : base(messageBus)
        {
        }

        public List<CubeModel> GetTower() =>
            _tower;

        public void AddCubeToTower(int viewId, int cubeId, Vector2 position)
        {
            var cubeModel = new CubeModel(cubeId, viewId, position);

            _tower.Add(cubeModel);
        }

        public int RemoveCubeFromTower(int viewId)
        {
            int index = _tower.FindIndex(x => x.ViewId == viewId);
            if (index >= 0) _tower.RemoveAt(index);
            return index;
        }
    }
}