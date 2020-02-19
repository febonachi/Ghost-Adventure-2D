using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : Item {
    #region editor
    [SerializeField] private KeyBlocker blocker;
    #endregion

    #region protected
    protected override void InteractionEnded() {
        base.InteractionEnded();

        blocker.gameObject.SetActive(false);
    }
    #endregion
}
