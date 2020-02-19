using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class NotUIButton : MonoBehaviour {
    [SerializeField]
    private UnityEvent OnClick;

    #region private
    private void OnMouseDown() {
        OnClick?.Invoke();
    }
    #endregion
}
