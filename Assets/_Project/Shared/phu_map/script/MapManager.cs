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
<<<<<<< HEAD
        [SerializeField] private PopScaleEffect img_shop;
        [SerializeField] private RectOffsetEffect globalPathEffect; // Hiệu ứng vẽ toàn bộ bản đồ
=======
        [SerializeField] private PopScaleEffect backbutton;
        [SerializeField] private RectOffsetEffect pathEffect;
>>>>>>> parent of dab5332 (Merge branch 'phu' of https://github.com/momytelloffmealots/LabDiner into phu)

        [Header("Danh sách Shop trong Map này")]
        public List<ShopNodeUI> shopNodes;

        [Header("Đường nối (Paths)")]
<<<<<<< HEAD
        [Tooltip("Kéo các đường nối có gắn RectOffsetEffect vào đây")]
        public List<RectTransform> paths; 
=======
        public List<Image> paths;
        public Image header_map;
>>>>>>> parent of dab5332 (Merge branch 'phu' of https://github.com/momytelloffmealots/LabDiner into phu)

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
<<<<<<< HEAD
                var node = shopNodes[i];
                if (node.shopData == null) continue;

                node.shopData.isCompleted = node.shopData.level < currentPlayerLevel;
                node.RefreshUI();

                // Logic xử lý đường nối (Paths)
                if (i < paths.Count && paths[i] != null)
                {
                    var effect = paths[i].GetComponent<RectOffsetEffect>();
                    int pathLevel = node.shopData.level+1;

                    if (pathLevel < currentPlayerLevel)
                    {
                        // ĐÃ VƯỢT QUA: Hiện luôn trạng thái cuối
                        paths[i].gameObject.SetActive(true);
                        if (effect != null) 
                        {
                            effect.FinishInstant(); 
                        }
                    }
                    else if (pathLevel == currentPlayerLevel)
                    {
                        // ĐANG Ở LEVEL NÀY: Vẽ đường nối dẫn tới level tiếp theo
                        paths[i].gameObject.SetActive(true);
                        if (effect != null) 
                        {
                            effect.StartEffect();
                        }
                    }
                    else
                    {
                        // CHƯA TỚI: Ẩn
                        paths[i].gameObject.SetActive(false);
                    }
                }
=======
                if (shopNodes[i].shopData == null) continue;
                shopNodes[i].shopData.isCompleted = shopNodes[i].shopData.level < currentPlayerLevel;
                shopNodes[i].RefreshUI();
>>>>>>> parent of dab5332 (Merge branch 'phu' of https://github.com/momytelloffmealots/LabDiner into phu)
            }
            paths[currentPlayerLevel - 1].DOMove()
        }
    }
}
