using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class RectOffsetEffect : MonoBehaviour
{
    public enum AnimationType
    {
        Left, Right, Top, Bottom, Horizontal, Vertical, All,
        Width,  // Thêm tùy chọn chỉnh Chiều rộng
        Height  // Thêm tùy chọn chỉnh Chiều cao
    }

    [Header("--- Cấu hình Animation ---")]
    public AnimationType targetType = AnimationType.Left;
    public float fromValue = 0f;
    public float toValue = 300f;
    public float duration = 0.5f;
    public Ease easeType = Ease.InOutQuad;

    [Header("--- Tùy chọn ---")]
    public bool playOnStart = false;
    public UnityEvent onComplete;

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        if (playOnStart) StartEffect();
    }

    public void StartEffect()
    {
        ApplyValue(fromValue);

        DOTween.To(() => fromValue, x => ApplyValue(x), toValue, duration)
            .SetTarget(this)
            .SetEase(easeType)
            .SetUpdate(true)
            .OnComplete(() => onComplete?.Invoke());
    }

    // Hàm hiện kết quả ngay lập tức không cần chờ animation
    public void FinishInstant()
    {
        ApplyValue(toValue);
    }

    private void ApplyValue(float value)
    {
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
        
        Vector2 min = rectTransform.offsetMin;
        Vector2 max = rectTransform.offsetMax;
        Vector2 size = rectTransform.sizeDelta;

        switch (targetType)
        {
            case AnimationType.Left: min.x = value; break;
            case AnimationType.Right: max.x = -value; break;
            case AnimationType.Top: max.y = -value; break;
            case AnimationType.Bottom: min.y = value; break;
            case AnimationType.Horizontal: min.x = value; max.x = -value; break;
            case AnimationType.Vertical: min.y = value; max.y = -value; break;
            case AnimationType.All:
                min = new Vector2(value, value);
                max = new Vector2(-value, -value);
                break;
            case AnimationType.Width:
                size.x = value;
                break;
            case AnimationType.Height:
                size.y = value;
                break;
        }

        if (targetType == AnimationType.Width || targetType == AnimationType.Height)
        {
            rectTransform.sizeDelta = size;
        }
        else
        {
            rectTransform.offsetMin = min;
            rectTransform.offsetMax = max;
        }
    }
}