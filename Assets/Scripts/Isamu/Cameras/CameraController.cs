using Isamu.Input;
using Isamu.Map;
using Isamu.Map.Navigation;
using Isamu.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Isamu.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform controllerTransform;
        
        // How quickly the camera moves when the cursor is by the edge of the screen.
        [SerializeField, Min(0f)] private float panSpeed = 7f;
        
        // How quickly the camera zooms in and out when the scroll wheel/trackpad receives input.
        [SerializeField, Min(0f)] private float zoomSpeed = 9f;
        
        // How close to the edge of the screen does the cursor need to be for the pan to occur.
        [SerializeField, Min(0f)] private float screenBorderThickness = 15f;
        
        // How zoomed in/out the camera can be.
        [SerializeField] private Vector2 screenYLimits = new(4f, 12f);
        
        // How far in any direction can the camera pan past the map borders.
        [Tooltip("(X min, X max, Z min, Z max)")]
        [SerializeField] private Vector4 screenLimitBuffers;
        
        private Vector2 _screenXLimits;
        private Vector2 _screenZLimits;

        private Controls _controls;

        private void Awake()
        {
            // Create our input Controls instance.
            _controls = new Controls();
            
            // Listen for scroll wheel input.
            _controls.Computer.ZoomCamera.performed += SetZoomInput;
            _controls.Computer.ZoomCamera.canceled += SetZoomInput;

            // Listen for when the map is generated.
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
            // Use the map size to set the pan limits.
            _screenXLimits = new Vector2(-screenLimitBuffers.x, mapAsset.Width + screenLimitBuffers.y);
            _screenZLimits = new Vector2(-screenLimitBuffers.z, mapAsset.Depth + screenLimitBuffers.w);
            
            // Subtract 1 since the tiles start at 0,0.
            // Position the camera at the half-way point of the map to start.
            controllerTransform.SetX((mapAsset.Width - 1) * 0.5f);
        }

        private void Update()
        {
            PanCamera();
        }

        private void PanCamera()
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
            
            // We could also use Transform.Translate but by calculating this manually, we can account for custom rotation.
            Vector3 newPos = controllerTransform.CalculateTranslation(translation);
            
            newPos.x = Mathf.Clamp(newPos.x, _screenXLimits.x, _screenXLimits.y);
            newPos.z = Mathf.Clamp(newPos.z, _screenZLimits.x, _screenZLimits.y);
            
            controllerTransform.position = newPos;
        }
        
        private void SetZoomInput(InputAction.CallbackContext ctx)
        {
            float zoom = ctx.ReadValue<float>();
            Vector3 pos = GameCamera.Instance.Transform.position;
            
            // Get a new position either forward or backwards on the camera's current facing.
            pos += GameCamera.Instance.Transform.TransformDirection(zoom * Time.deltaTime * zoomSpeed * Vector3.forward);

            // Only zoom the camera if doing so would not move it past one of the limits.
            if (!(pos.y <= screenYLimits.x) && !(pos.y >= screenYLimits.y))
            {
                GameCamera.Instance.Transform.position = pos;
            }
        }
    }
}
