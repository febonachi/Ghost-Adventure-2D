using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {
    #region editor
    [SerializeField] private Transform target;
    #endregion

    private Vector2 offset;

    #region private
    private void Start() {
        offset = transform.position - target.position;
    }

    private void Update() {
        transform.position = (Vector2)target.transform.position + offset;
    }
    #endregion
}
