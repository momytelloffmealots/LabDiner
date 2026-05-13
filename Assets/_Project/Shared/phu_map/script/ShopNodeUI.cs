    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using DG.Tweening;

    namespace LabDiner.Shared
    {

        public class ShopNodeUI : MonoBehaviour
        {
            public ShopData shopData;

            [Header("UI References")]
            [SerializeField] private Image shopIconImage;
            [SerializeField] private Image destination;
            [SerializeField] private GameObject panel;
            [SerializeField] private TextMeshProUGUI nameText;

            // Chạy khi nhấn Play
            private void Start()
            {
                RefreshUI();
            }

            // Hàm này để MapManager gọi hoặc dùng để cập nhật thủ công
            public void RefreshUI()
            {
                if (shopData == null)
                {
                    return;
                }

                // Cập nhật thông tin từ ScriptableObject
                if (shopIconImage != null) shopIconImage.sprite = shopData.icon;
                if (nameText != null) nameText.text = shopData.shopName;
                if (panel != null)
                {
                var panelImg = panel.GetComponent<Image>();
                if (panelImg != null) panelImg.sprite = shopData.panel;

                // Chỉ ẩn panel lúc khởi tạo, không để chung dòng với lệnh gán sprite
                panel.gameObject.SetActive(false);
                }
            if (destination != null)      destination.sprite = shopData.destination;
                if (shopIconImage != null) shopIconImage.color = shopData.isCompleted ? Color.white : Color.brown;
            }

            public void OnShopClick()
            {
                if (panel == null) return;

                // Kiểm tra trạng thái hiện tại của Panel để Toggle (Đảo ngược trạng thái)
                bool isActive = panel.gameObject.activeSelf;
                panel.gameObject.SetActive(!isActive);

            // Nếu là mở Panel thì mới cần cập nhật dữ liệu từ ShopData
            if (!isActive)
            {
                var panelImg = panel.GetComponent<Image>();
                if (panelImg != null) panelImg.sprite = shopData.panel;
            }
        }
    }
    }
