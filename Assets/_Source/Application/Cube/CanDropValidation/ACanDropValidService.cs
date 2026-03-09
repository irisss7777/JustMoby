using System.Collections.Generic;
using Domain.Cube;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;

namespace Application.Cube.CanDropValidation
{
    public abstract class ACanDropValidService : AService
    {
        public ACanDropValidService(MessageBus messageBus) : base(messageBus)
        {
        }

        public abstract bool CanDrop(int viewId, int cubeId, Vector2 position, List<CubeModel> tower, bool isOther);
    }
}