using UnityEngine;

public class KeyBlocker : InteractiveEntity {
    #region private
    [SerializeField] private float damage = 5f;
    [SerializeField] private float strength = .2f;
    #endregion

    #region protected
    protected override void Initialize() {
        base.Initialize();
    }

    protected override void Interact() {
        player.TakeDamage(damage);
        player.AddForce(strength);
    }
    #endregion

    #region public
    public void OnItemDataChanged(ItemOrder order) {
        Destroy(gameObject);
    }
    #endregion
}
