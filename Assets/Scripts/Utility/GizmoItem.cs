using UnityEngine;

public class GizmoItem : MonoBehaviour {
    [Range(0f, 10f)]
    [SerializeField]
    protected float visibleRange = 1f;

    #region private
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visibleRange);
    }
    #endregion
}
