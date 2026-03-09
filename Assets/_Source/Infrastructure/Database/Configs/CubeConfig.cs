using System;
using Contracts.PresentationContracts.Database.Config;
using UnityEngine;

namespace Infrastructure.Database.Configs
{
    [Serializable]
    public class CubeConfig : ICubeConfig
    {
        [SerializeField] private Sprite _cubeSprite;
        public Sprite CubeSprite => _cubeSprite;
    }
}