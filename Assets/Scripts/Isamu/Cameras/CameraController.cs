using System.Collections.Generic;
using Isamu.Input;
using Isamu.Map;
using Isamu.Map.Navigation;
using Isamu.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Isamu.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform controllerTransform;
        [SerializeField, Min(0f)] private float panSpeed = 7f;
        [SerializeField, Min(0f)] private float zoomSpeed = 9f;
        [SerializeField, Min(0f)] private float screenBorderThickness = 15f;
        [SerializeField] private Vector2 screenYLimits = new(4f, 12f);
        
        [Tooltip("(X min, X max, Z min, Z max)")]
        [SerializeField] private Vector4 screenLimitBuffers;
        
        private Vector2 _screenXLimits;
        private Vector2 _screenZLimits;

        private Controls _controls;

        private void Awake()
        {
            _controls = new Controls();
            _controls.Computer.ZoomCamera.performed += SetZoomInput;
            _controls.Computer.ZoomCamera.canceled += SetZoomInput;

            MapGenerator.OnMapGenerated += HandleMapCreated;
            _controls.Enable();
        }

        private void OnDestroy()
        {
            _controls.Computer.ZoomCamera.performed -= SetZoomInput;
            _controls.Computer.ZoomCamera.canceled -= SetZoomInput;

            MapGenerator.OnMapGenerated -= HandleMapCreated;
            _controls.Disable();
        }

        private void HandleMapCreated(MapAsset mapAsset, List<NavigationNode> navigationNodes)
        {
            _screenXLimits = new Vector2(-screenLimitBuffers.x, mapAsset.Width + screenLimitBuffers.y);
            _screenZLimits = new Vector2(-screenLimitBuffers.z, mapAsset.Depth + screenLimitBuffers.w);
            
            // Subtract 1 since the tiles start at 0,0.
            // Position the camera at the half-way point of the map to start.
            controllerTransform.SetX((mapAsset.Width - 1) * 0.5f);
        }

        private void Update()
        {
            Vector3 cursorMovement = Vector3.zero;
            Vector2 cursorPos = Mouse.current.position.ReadValue();
            
            if (cursorPos.y >= Screen.height - screenBorderThickness)
            {
                cursorMovement.z += 1;
            }
            else if (cursorPos.y < screenBorderThickness)
            {
                cursorMovement.z -= 1;
            }
            
            if (cursorPos.x >= Screen.width - screenBorderThickness)
            {
                cursorMovement.x += 1;
            }
            else if (cursorPos.x < screenBorderThickness)
            {
                cursorMovement.x -= 1;
            }
            
            Vector3 translation = cursorMovement * Time.deltaTime * panSpeed;
            Vector3 newPos = controllerTransform.CalculateTranslation(translation);
            
            newPos.x = Mathf.Clamp(newPos.x, _screenXLimits.x, _screenXLimits.y);
            newPos.z = Mathf.Clamp(newPos.z, _screenZLimits.x, _screenZLimits.y);
            
            controllerTransform.position = newPos;
        }
        
        private void SetZoomInput(InputAction.CallbackContext ctx)
        {
            float zoom = ctx.ReadValue<float>();
            Vector3 pos = GameCamera.Instance.Transform.position;
            pos += GameCamera.Instance.Transform.TransformDirection(zoom * Time.deltaTime * zoomSpeed * Vector3.forward);

            if (!(pos.y <= screenYLimits.x) && !(pos.y >= screenYLimits.y))
            {
                GameCamera.Instance.Transform.position = pos;
            }
        }
    }
}
