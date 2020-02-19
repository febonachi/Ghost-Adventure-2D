using UnityEngine;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour {
    [SerializeField]
    private Image image;
    [SerializeField]
    private ParticleSystem ps;

    private RectTransform rect;

    #region private
    private void Start() {
        rect = image.GetComponent<RectTransform>();
    }
    #endregion

    #region public
    public void SetupItem(ItemData itemData) {
        image.enabled = true;

        rect.localPosition = itemData.itemHolderPos;
        rect.sizeDelta = new Vector2(itemData.itemHolderSize.width, itemData.itemHolderSize.height);
        image.sprite = itemData.itemSprite;
        image.color = itemData.itemColor;

        Destroy(Instantiate(ps, UIController.instance.UIToWorldPoint(transform.position), Quaternion.identity).gameObject, 2f);
    }
    #endregion
}
