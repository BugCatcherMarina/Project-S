using UnityEngine;
using UnityEngine.Events;

public class EnableDisableBehaviour : MonoBehaviour
{
    [SerializeField] private UnityEvent onEnable; 
    [SerializeField] private UnityEvent onDisable;

    private void OnEnable()
    {
        onEnable?.Invoke();
    }

    private void OnDisable()
    {
        onDisable?.Invoke();
    }
}
