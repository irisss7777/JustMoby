using System.Collections.Generic;
using Contracts.ApplicationContracts.Signal;
using Domain.Cube;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;

namespace Application.Cube.CanDropValidation
{
    public class RightPanelDropValidService : ACanDropValidService
    {
        private bool _inRightPanel;
        
        public RightPanelDropValidService(MessageBus messageBus) : base(messageBus)
        {
            this.Subscribe(MessageBus, (PointerInRightPanelSignal signal) => _inRightPanel = signal.InPanel);
        }

        public override bool CanDrop(int viewId, int cubeId, Vector2 position, List<CubeModel> tower, bool isOther)
        {
            if (isOther)
                return true;
            
            return _inRightPanel;
        }
    }
}