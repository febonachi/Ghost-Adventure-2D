using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName ="Items/Item", order = 1)]
public class ItemData : ScriptableObject {
    public ItemOrder order;
    public new string name;

    // Item data
    public Sprite itemSprite;
    public Color itemColor = Color.white;
    public Vector2 itemHolderPos;
    public Size2d itemHolderSize;

    // Hint data
    public Sprite hintSprite;
    public Color hintColor = Color.white;
    public int hintCost = 0;
    public Vector2 hintHolderPos;
    public Size2d hintHolderSize;
}
