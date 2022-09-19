using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ProjectS.General
{
    [RequireComponent(typeof(Collider))]
    public class InteractableBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private UnityEvent onPointerEnter;
        [SerializeField] private UnityEvent onPointerExit;
        [SerializeField] private UnityEvent onPointerClick;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            onPointerEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onPointerExit?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onPointerClick?.Invoke();
        }
    }
}