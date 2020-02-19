using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MiniGameGridCell : MonoBehaviour {

    private MiniGameGrid grid;
    private new SpriteRenderer renderer;

    #region private
    private void Awake() {
        renderer = GetComponent<SpriteRenderer>();
        grid = transform.parent.GetComponent<MiniGameGrid>();
    }

    private void SwapSprites(MiniGameGridCell other) {
        Sprite tmp = renderer.sprite;
        renderer.sprite = other.renderer.sprite;
        other.renderer.sprite = tmp;
    }
    #endregion

    #region public
    public void SetupSprite(Sprite sprite) {
        renderer.sprite = sprite;
    }

    public void Swap(MiniGameGridCell other) {
        SwapSprites(other);
    }

    public bool Compare(Sprite other) {
        return renderer.sprite == other;
    }
    #endregion
}
