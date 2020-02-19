using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnZone))]
public class BushEnergy : MonoBehaviour {
    #region editor
    [SerializeField] private ParticleSystem psWhenTaken;
    [SerializeField] private Color energyColor;
    #endregion

    private SpawnZone sz;

    #region private
    private void Start() {
        sz = GetComponent<SpawnZone>();
    }

    private int TakeEnergy() {
        int count = sz.EnergyCount;
        sz.ClearAndDestroy();
        Destroy(Instantiate(psWhenTaken, sz.transform).gameObject, 1f);
        return count;
    }
    #endregion

    #region public 
    public int Interact(TwoStateInt count, int chance) {
        if (sz.EnergyCount > 0) return TakeEnergy();
        bool initialize = Utils.Random(0, 100) < chance ? true : false;
        if (initialize) sz.Initialize(Utils.Random(count.first, count.second), energyColor);
        return 0;
    }
    #endregion
}
