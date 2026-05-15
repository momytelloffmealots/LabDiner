using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using LabDiner.Shared.UI;

namespace LabDiner.Shared
{
    public class ShopNodeUI : MonoBehaviour
    {
        public ShopData shopData;

        [Header("UI References")]
        [SerializeField] private Image shopIconImage;
        [SerializeField] private Image destination;
        [SerializeField] private GameObject checkmark;
        [SerializeField] private GameObject panel;
        [SerializeField] private TextMeshProUGUI nameText;

        private void Start()
        {
            RefreshUI();
        }

        public void RefreshUI()
        {
            if (shopData == null) 
            {
                Debug.LogWarning($"[ShopNodeUI] {gameObject.name} thiếu ShopData!");
                return;
            }

            if (shopIconImage != null) shopIconImage.sprite = shopData.icon;
            if (nameText != null) nameText.text = shopData.shopName;
            if (destination != null) destination.sprite = shopData.destination;

            if (checkmark != null)
                checkmark.SetActive(shopData.isCompleted);

            if (shopIconImage != null) 
                shopIconImage.color = shopData.isCompleted ? Color.white : Color.gray;

            if (panel != null)
            {
                var panelImg = panel.GetComponent<Image>();
                if (panelImg != null) panelImg.sprite = shopData.panel;
                panel.gameObject.SetActive(false);
            }
        }

        public void OnShopClick()
        {
            Debug.Log($"[ShopNodeUI] Đã click vào shop: {gameObject.name}");

            if (panel == null) 
            {
                Debug.LogError($"[ShopNodeUI] {gameObject.name} chưa kéo Panel vào Inspector!");
                return;
            }

            if (shopData == null) return;

            var popEffect = panel.GetComponent<PopScaleEffect>();
            bool isActive = panel.activeSelf;

            if (isActive)
            {
                Debug.Log("Đang đóng Panel...");
                if (popEffect != null)
                {
                    popEffect.Hide(() => panel.SetActive(false));
                }
                else
                {
                    panel.SetActive(false);
                }
            }
            else
            {
                Debug.Log("Đang mở Panel...");
                panel.SetActive(true);

                var panelImg = panel.GetComponent<Image>();
                if (panelImg != null) panelImg.sprite = shopData.panel;

                if (popEffect != null)
                {
                    popEffect.Show();
                }
                else
                {
                    Debug.LogWarning($"[ShopNodeUI] Panel trên {gameObject.name} không có script PopScaleEffect!");
                }
            }
        }
    }
}
