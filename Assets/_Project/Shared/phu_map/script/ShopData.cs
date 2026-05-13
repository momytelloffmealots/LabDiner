using UnityEngine;

[CreateAssetMenu(fileName = "NewShop", menuName = "SagaMap/Shop Data")]
public class ShopData : ScriptableObject
{
    [Header("Thông tin cơ bản")]
    public string shopName;         // Tên shop (ví dụ: Little Cafe)
    public Sprite destination;         // chủ đề map
    public Sprite icon;                // icon hiển thị trên map
    public int level;       // Cấp độ
    public Sprite panel;               // panel hiển thị khi nhấn vào shop

    [Header("Trạng thái")]
    public bool isCompleted;        // Đã hoàn thành (có dấu tick xanh) chưa?


}