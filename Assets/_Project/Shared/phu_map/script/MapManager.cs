using UnityEngine;
using UnityEngine.UI;   
using System.Collections.Generic;
using DG.Tweening;
using LabDiner.Shared.UI;

namespace LabDiner.Shared
{
    public class MapManager : MonoBehaviour
    {
        [Header("Animation Settings")]
        [SerializeField] private PopScaleEffect _mainPanelEffect; // Hiệu ứng cho toàn bộ Map Panel
        [SerializeField] private PopScaleEffect backbutton;
        [SerializeField] private RectOffsetEffect pathEffect;

        [Header("Danh sách Shop trong Map này")]
        public List<ShopNodeUI> shopNodes;

        [Header("Đường nối (Paths)")]
        public List<Image> paths;
        public Image header_map;

        [Header("Player Progress")]
        [SerializeField] private int playerLevel = 1; // Cấp hiện tại của người chơi

        // Hàm này gọi khi ấn nút Mở Map
        public void OpenMap()
        {
            // 1. Hiện Panel chính
            if (_mainPanelEffect != null)
            {
                _mainPanelEffect.gameObject.SetActive(true);
                _mainPanelEffect.Show();
            }

            // 2. Chạy hiệu ứng cho Header/Path (Nếu có)
            if (pathEffect != null)
            {
                pathEffect.gameObject.SetActive(true);
                pathEffect.StartEffect();
            }

            // 3. Hiện nút Back
            if (backbutton != null)
            {
                backbutton.gameObject.SetActive(true);
                backbutton.Show();
            }

            // 4. Cập nhật trạng thái các shop
            RefreshMap(playerLevel);
        }

        // Hàm này gọi khi ấn nút Đóng Map
        public void CloseMap()
        {
            if (_mainPanelEffect != null)
            {
                _mainPanelEffect.Hide(() => _mainPanelEffect.gameObject.SetActive(false));
            }
            
            if (backbutton != null) backbutton.Hide();
        }

        public void RefreshMap(int currentPlayerLevel)
        {
            for (int i = 0; i < shopNodes.Count; i++)
            {
                if (shopNodes[i].shopData == null) continue;
                shopNodes[i].shopData.isCompleted = shopNodes[i].shopData.level < currentPlayerLevel;
                shopNodes[i].RefreshUI();
            }
        }
    }
}
