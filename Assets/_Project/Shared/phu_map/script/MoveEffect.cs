using DG.Tweening;
using UnityEngine;

namespace LabDiner.Shared
{
    public class MoveEffect : MonoBehaviour
    {
        [SerializeField] private RectTransform objectToMove; // Vật thể muốn di chuyển
        [SerializeField] private RectTransform target;       // Đích đến

        public void MoveToTarget()
        {
            // Kiểm tra xem đã kéo đủ Object vào chưa để tránh lỗi Null
            if (objectToMove == null || target == null)
            {
                Debug.LogError("Chưa kéo đủ Object vào MoveEffect kìa bạn ơi!");
                return;
            }

            // 1. Dừng các hành động di chuyển cũ
            objectToMove.DOKill();

            // 2. Di chuyển UI đến vị trí đích (dùng anchoredPosition cho chuẩn UI)
            objectToMove.DOAnchorPos(target.anchoredPosition, 1f)
                        .SetEase(Ease.OutQuart)
                        .SetDelay(2f)
                        .SetUpdate(true); // Để nó chạy được kể cả khi game đang pause

            Debug.Log("Đang di chuyển đến: " + target.name);
        }
    }
}