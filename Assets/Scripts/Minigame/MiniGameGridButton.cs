using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class MiniGameGridButton : MonoBehaviour {

    public UnityEvent clicked;

    #region private
    private void OnMouseDown() {
        clicked.Invoke();
    }
    #endregion
}
