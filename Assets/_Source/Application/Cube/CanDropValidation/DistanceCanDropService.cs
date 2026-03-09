using System;
using System.Collections.Generic;
using Domain.Cube;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;

namespace Application.Cube.CanDropValidation
{
    public class DistanceCanDropService : ACanDropValidService
    {
        private readonly float _distanceBetweenCube;
        private readonly float _verticalDistanceBetweenCube;
        
        public DistanceCanDropService(MessageBus messageBus, float distanceBetweenCube, float verticalDistanceBetweenCube) : base(messageBus)
        {
            _distanceBetweenCube = distanceBetweenCube;
            _verticalDistanceBetweenCube = verticalDistanceBetweenCube;
        }

        public override bool CanDrop(int viewId, int cubeId, Vector2 position, List<CubeModel> tower, bool isOther)
        {
            if (tower.Count == 0)
                return true;

            if (Math.Abs(position.x - tower[^1].Position.x) > _distanceBetweenCube)
                return false;

            if (Math.Abs(position.y - tower[^1].Position.y) < _verticalDistanceBetweenCube)
                return false;

            return true;
        }
    }
}