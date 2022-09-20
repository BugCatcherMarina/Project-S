using Isamu.Input;
using Isamu.Map;
using Isamu.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Isamu.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform controllerTransform;
        [SerializeField] private float moveSpeed = 8f;
        [SerializeField] private float zoomSpeed = 10f;
        [SerializeField] private float screenBorderThickness = 20f;
        [SerializeField] private Vector2 screenYLimits = new(4f, 12f);
        [SerializeField] private int screenLimitBuffer;
        private Vector2 screenXLimits;
        private Vector2 screenZLimits;

        private Controls controls;

        private void Awake()
        {
            MapGenerator.OnMapCreated += HandleMapCreated;
            controls = new Controls();
            controls.Computer.ZoomCamera.performed += SetZoomInput;
            controls.Computer.ZoomCamera.canceled += SetZoomInput;
            
            controls.Enable();
        }

        private void OnDestroy()
        {
            controls.Computer.ZoomCamera.performed -= SetZoomInput;
            controls.Computer.ZoomCamera.canceled -= SetZoomInput;
            controls.Disable();
        }

        private void HandleMapCreated(MapAsset mapAsset)
        {
            screenXLimits = new Vector2(0 - screenLimitBuffer, mapAsset.Width + screenLimitBuffer);
            screenZLimits = new Vector2(0 - screenLimitBuffer, mapAsset.Depth + screenLimitBuffer);
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
            
            Vector3 translation = cursorMovement * Time.deltaTime * moveSpeed;
            Vector3 newPos = controllerTransform.CalculateTranslation(translation);
            
            newPos.x = Mathf.Clamp(newPos.x, screenXLimits.x, screenXLimits.y);
            newPos.z = Mathf.Clamp(newPos.z, screenZLimits.x, screenZLimits.y);
            
            controllerTransform.position = newPos;
        }
        
        private void SetZoomInput(InputAction.CallbackContext ctx)
        {
            float zoom = ctx.ReadValue<float>();
            Vector3 pos = GameCamera.Instance.Transform.position;
            pos += GameCamera.Instance.Transform.TransformDirection(zoom * Time.deltaTime * zoomSpeed * Vector3.forward);
            
            if (pos.y <= screenYLimits.x || pos.y >= screenYLimits.y)
            {
                return;
            }

            GameCamera.Instance.Transform.position = pos;
        }
    }
}
