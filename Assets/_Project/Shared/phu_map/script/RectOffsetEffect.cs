using UnityEngine;
using UnityEngine.Events;
using DG.Tweening; // Thêm DOTween

[RequireComponent(typeof(RectTransform))]
public class RectOffsetEffect : MonoBehaviour
{
    public enum OffsetSide
    {
        Left, Right, Top, Bottom, Horizontal, Vertical, All
    }

    [Header("--- Cấu hình Offset ---")]
    public OffsetSide targetSide = OffsetSide.Left;
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
        // Reset về giá trị bắt đầu
        ApplyOffset(fromValue);

        // Sử dụng DOTween để chạy hiệu ứng (Thay thế Coroutine)
        DOTween.To(() => fromValue, x => ApplyOffset(x), toValue, duration)
            .SetTarget(this)
            .SetEase(easeType)
            .SetUpdate(true) // Chạy cả khi pause
            .OnComplete(() => onComplete?.Invoke());
    }

    private void ApplyOffset(float value)
    {
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
        
        Vector2 min = rectTransform.offsetMin;
        Vector2 max = rectTransform.offsetMax;

        switch (targetSide)
        {
            case OffsetSide.Left: min.x = value; break;
            case OffsetSide.Right: max.x = -value; break;
            case OffsetSide.Top: max.y = -value; break;
            case OffsetSide.Bottom: min.y = value; break;
            case OffsetSide.Horizontal: min.x = value; max.x = -value; break;
            case OffsetSide.Vertical: min.y = value; max.y = -value; break;
            case OffsetSide.All:
                min = new Vector2(value, value);
                max = new Vector2(-value, -value);
                break;
        }
        rectTransform.offsetMin = min;
        rectTransform.offsetMax = max;
    }
}