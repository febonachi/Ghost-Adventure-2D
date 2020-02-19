using UnityEngine;

public class PipesItem : Item {
    #region editor
    [SerializeField] private GameObject teapot;
    #endregion

    #region protected
    protected override void Initialize() {
        base.Initialize();
    }

    protected override void TakeItem() {
        if (itemPs) itemPs.Stop();
        StartCoroutine(Utils.DissolveOverLifeTime(teapot.GetComponent<SpriteRenderer>()));
    }

    protected override void InteractionEnded() {
        base.InteractionEnded();

        item.GetComponent<ParticleSystem>().Stop();
    }
    #endregion
}
