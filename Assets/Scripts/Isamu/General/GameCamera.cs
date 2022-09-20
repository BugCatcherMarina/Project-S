using Isamu.Utils;
using UnityEngine;

namespace Isamu.General
{
    [RequireComponent(typeof(Camera))]
    public class GameCamera : Singleton<GameCamera>
    {
        public Transform Transform
        {
            get
            {
                if (_transform == null)
                {
                    _transform = transform;
                }

                return _transform;
            }
        }
        
        private Transform _transform;
        
        public Camera Cam { get; private set; }
        
        protected override void OnAwake()
        {
            base.OnAwake();
            Cam = GetComponent<Camera>();
        }
    }
}
