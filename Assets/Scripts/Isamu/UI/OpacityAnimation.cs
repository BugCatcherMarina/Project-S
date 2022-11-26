using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class OpacityAnimation : MonoBehaviour
{
    [SerializeField, Range(0.000f, 0.999f)] private float opacityMin = 0f;
    [SerializeField, Range(0.001f, 1.000f)] private float opacityMax = 1f;
    [SerializeField] private float duration;

    private CanvasGroup canvasGroup;

    private float unscaledTime;

    public void StartAnimation()
    {
        unscaledTime = 0f;
        SetOpacity(opacityMin);
        enabled = true;
    }

    public void StopAnimation()
    {
        SetOpacity(opacityMin);
        enabled = false;
    }

    private void Update()
    {
        unscaledTime += Time.unscaledDeltaTime;
        float newOpacity = Mathf.PingPong(unscaledTime, opacityMax - opacityMin);
        SetOpacity(newOpacity + opacityMin);
    }

    private void SetOpacity(float opacity)
    {
        canvasGroup.alpha = opacity;
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
}
