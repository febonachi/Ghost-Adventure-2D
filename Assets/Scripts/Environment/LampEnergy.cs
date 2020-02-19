using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampEnergy : MonoBehaviour {
    #region editor
    [SerializeField] private ParticleSystem ps;
    #endregion

    public Color EnergyColor { get; set; }

    #region public
    public void ChangeColor(Color c) {
        EnergyColor = c;

        ParticleSystem.MainModule main = ps.main;
        main.startColor = EnergyColor;
    }
    #endregion
}
