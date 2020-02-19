using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameLamp : MonoBehaviour {

    [SerializeField] private SpawnZone spawnZone;

    #region private
    private void Start() {
        spawnZone.Initialize(30, Color.white);
    }
    #endregion
}
