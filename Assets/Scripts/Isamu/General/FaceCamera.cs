using Isamu.Utils;
using UnityEngine;

namespace Isamu.General
{
    public class FaceCamera : MonoBehaviour
    {
        [SerializeField] private Transform _transform;

        private Transform _camTransform;
        
        private void Start()
        {
            _camTransform = GameCamera.Instance.Transform;
        }

        private void LateUpdate()
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }

            Quaternion camRotation = _camTransform.rotation;
            _transform.LookAt(_transform.position + camRotation * Vector3.forward, camRotation * Vector3.up);
        }
    }
}
