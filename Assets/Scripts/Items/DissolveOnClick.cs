using UnityEngine;

public class DissolveOnClick : InteractiveEntity {
    #region editor
    [SerializeField] private SpriteRenderer sr;
    #endregion

    #region protected
    protected override void Interact() {
        StartCoroutine(Utils.DissolveOverLifeTime(sr));
    }
    #endregion
}
