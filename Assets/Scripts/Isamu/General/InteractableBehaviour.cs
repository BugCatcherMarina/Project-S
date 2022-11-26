using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Isamu.General
{
    public class InteractableBehaviour : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerClickHandler,
        IPointerDownHandler,
        IPointerUpHandler
    {
        [SerializeField] private bool ignoreEnterExitIfPointerDown = true;

        [SerializeField] private UnityEvent onPointerEnter;
        [SerializeField] private UnityEvent onPointerExit;
        [SerializeField] private UnityEvent onPointerDown;
        [SerializeField] private UnityEvent onPointerUp;
        [SerializeField] private UnityEvent onPointerClick;

        private bool isPointerDown;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerPress == null || !ignoreEnterExitIfPointerDown)
            {
                onPointerEnter?.Invoke();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isPointerDown || !ignoreEnterExitIfPointerDown)
            {
                onPointerExit?.Invoke();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onPointerClick?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isPointerDown = true;
            onPointerDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPointerDown = false;
            onPointerUp?.Invoke();
        }
    }
}
