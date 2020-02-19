using UnityEngine;

public class Lantern : MonoBehaviour {
    #region editor
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ParticleSystem effectPs;
    #endregion

    public Color LanternColor {
        get => spriteRenderer.color;
        set => spriteRenderer.color = value;
    }

    #region public
    public void OnLanternButtonClick() {
        Destroy(Instantiate(effectPs, transform).gameObject, 3f);
    }
    #endregion
}