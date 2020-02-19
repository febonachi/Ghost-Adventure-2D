using UnityEngine;

public class Bush : InteractiveEntity {
    #region editor
    [SerializeField] private BushEnergy bushEnergy;
    [SerializeField] private TwoStateInt energyCount;
    [SerializeField] private float energyCost = 1f;
    [SerializeField] private int chance = 2;
    [SerializeField] private ChanceSpawner shellSpawner;
    #endregion

    #region protected
    protected override void Interact() {
        base.Interact();

        int taken = bushEnergy.Interact(energyCount, chance);
        if (taken > 0) player.AddEnergy(energyCost * taken);

        shellSpawner?.Spawn();
    }
    #endregion
}
