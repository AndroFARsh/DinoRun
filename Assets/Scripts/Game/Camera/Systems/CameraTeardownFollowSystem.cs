using Leopotam.EcsLite;
using UnityEngine;

namespace Game
{
    public class CameraTeardownFollowSystem : IEcsDestroySystem
    {
        private readonly Transform _cameraTransform;

        CameraTeardownFollowSystem(Camera camera)
        {
            _cameraTransform = camera.transform;
        }
        
        public void Destroy(IEcsSystems systems)
        {
            _cameraTransform.position = new Vector3(0, 0, _cameraTransform.position.z);
        }
    }
}