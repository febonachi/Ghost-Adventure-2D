using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : InteractiveEntity {

    #region editor
    [SerializeField] private Transform arrow;
    [SerializeField] private float speed = 15f;
     #endregion

    private Transform nextTarget;

    #region private
    private float AngleTo(Transform target) {
        Vector3 dir = arrow.position - target.position;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    private bool FindNextTarget() {
        if (Item.ItemId <= ItemOrder.Key) {
            Item item = GameObject.FindObjectsOfType<Item>().First(e => e.Order() == Item.ItemId + 1);
            nextTarget = item?.transform;
        } else {
            nextTarget = GameObject.FindObjectOfType<ExitDoor>()?.transform;
        }
        return nextTarget ? true : false;
    }
    #endregion

    #region protected
    protected override void Initialize() {
        base.Initialize();

        //Interact();
    }

    protected override void Interact() {
        base.Interact();

        if (FindNextTarget()) {
            StopAllCoroutines();
            float angle = 360f + (360f - AngleTo(nextTarget) + (Mathf.Abs(arrow.localEulerAngles.z) - 360f));
            StartCoroutine(Utils.RotateOverLifeTime(arrow.transform, -Vector3.forward, angle, speed));
        }
    }
    #endregion
}
