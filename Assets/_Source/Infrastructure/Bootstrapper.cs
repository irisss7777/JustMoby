using Infrastructure.Factory.Cube;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        [Inject] private readonly CubeUiFactory _cubeUiFactory;

        private void Awake()
        {
            _cubeUiFactory.CreateCubeUi();
        }
    }
}