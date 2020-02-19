using UnityEngine;

public class Mushroom : InteractiveEntity {
    [SerializeField]
    private ParticleSystem ps;

    #region protected
    protected override void Initialize() {
        base.Initialize();

        ps.Stop();
    }

    protected override void Interact() {
        base.Interact();

        ps.Play();
    }
    #endregion
}
