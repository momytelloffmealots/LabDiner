using UnityEngine;
using UnityEngine.UI;   
using System.Collections.Generic;
using DG.Tweening;
namespace LabDiner.Shared
{
    public class MapManager : MonoBehaviour
    {
        [Header("Danh sách Shop trong Map này")]
        public List<ShopNodeUI> shopNodes;

        [Header("Đường nối (Paths)")]
        public List<Image> paths;
        public Image header_map;
        [Header("Player Progress")]
        [SerializeField] private int playerLevel = 1; // Cấp hiện tại của người chơi

        public void OpenMap()
        {
            // 1. Bật cái GameObject chứa bản đồ lên
            // (Giả sử bạn gán cái Object 'map' trong Hierarchy vào biến mapPanel này)
            // mapPanel.SetActive(true); 

            // 2. Gọi Refresh để chạy animation từ đầu
            RefreshMap(playerLevel);
        }

        public void RefreshMap(int currentPlayerLevel)
        {
            // RESET TOÀN BỘ TRƯỚC KHI TÍNH TOÁN
            foreach (var path in paths)
            {
                path.DOKill();
                path.fillAmount = 0f; // Đưa về 0 để chắc chắn animation chạy từ đầu
            }
            
            if (header_map != null)
            {
                header_map.DOKill();
                header_map.fillAmount = 0f;
            }

            // Logic xử lý Shop (Giữ nguyên của bạn)
            for (int i = 0; i < shopNodes.Count; i++)
            {
                if (shopNodes[i].shopData == null) continue;
                shopNodes[i].shopData.isCompleted = shopNodes[i].shopData.level < currentPlayerLevel;
                shopNodes[i].RefreshUI();
            }
            header_map.DOFillAmount(1f, 1f).SetEase(Ease.Linear);
            // Logic chạy Animation cho Path
            for (int i = 0; i < paths.Count; i++)
            {
                int targetLevel = i + 3; // Giữ nguyên +3 theo ý bạn

                if (targetLevel <= currentPlayerLevel)
                {
                    paths[i].fillAmount = 1f; // Những đường cũ hiện luôn
                }
                else if (targetLevel == currentPlayerLevel + 1)
                {
                    // Chỉ chạy animation cho đúng đường nối dẫn đến màn mới
                    paths[i].DOFillAmount(1f, 1f).SetEase(Ease.Linear);
                }
            }
        }
    }
}
