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
        [SerializeField] private PopScaleEffect img_shop;
        [SerializeField] private RectOffsetEffect globalPathEffect; // Hiệu ứng vẽ toàn bộ bản đồ
        [SerializeField] private MoveEffect OtoMove;
        [Header("Danh sách Shop trong Map này")]
        public List<ShopNodeUI> shopNodes;

        [Header("Đường nối (Paths)")]
        [Tooltip("Kéo các đường nối có gắn RectOffsetEffect vào đây")]
        public List<RectTransform> paths;

        [Header("Player Progress")]
        [SerializeField] private int playerLevel = 1; // Cấp hiện tại của người chơi

        public void OpenMap()
        {
            if (_mainPanelEffect != null)
            {
                _mainPanelEffect.gameObject.SetActive(true);
                _mainPanelEffect.Show();
            }

            if (globalPathEffect != null)
            {
                globalPathEffect.gameObject.SetActive(true);
                globalPathEffect.StartEffect();
            }

            if (img_shop != null)
            {
                img_shop.gameObject.SetActive(true);
                img_shop.Show();
            }

            RefreshMap(playerLevel);
        }

        public void CloseMap()
        {
            if (_mainPanelEffect != null)
            {
                _mainPanelEffect.Hide(() => _mainPanelEffect.gameObject.SetActive(false));
            }
            if (img_shop != null) img_shop.Hide();
        }

        public void RefreshMap(int currentPlayerLevel)
        {
            for (int i = 0; i < shopNodes.Count; i++)
            {
                var node = shopNodes[i];
                if (node.shopData == null) continue;

                node.shopData.isCompleted = node.shopData.level < currentPlayerLevel;
                node.RefreshUI();
                // Logic xử lý đường nối (Paths)
                if (i < paths.Count && paths[i] != null)
                {
                    var effect = paths[i].GetComponent<RectOffsetEffect>();
                    int pathLevel = node.shopData.level + 1;

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
                if (i == currentPlayerLevel - 2) {
                    OtoMove.gameObject.SetActive(true);
                    OtoMove.MoveToTarget();
                }
            }
        }
    }
}

