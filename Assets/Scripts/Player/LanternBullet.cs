using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class LanternBullet : MonoBehaviour {

    #region editor
    public GameObject explode;
    public ParticleSystem trailPS;
    #endregion

    private Rigidbody2D rb;
    private Vector3 targetPointOffset;

    #region private
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private IEnumerator MoveTo(Enemy enemy) {
        Vector3 targetPoint = enemy.transform.position - targetPointOffset;
        float distance = Vector3.Distance(transform.position, targetPoint);
        while (distance >= .1f && enemy != null){
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, Time.deltaTime * 8);
            targetPoint = enemy.transform.position - targetPointOffset;
            distance = Vector3.Distance(transform.position, targetPoint);
            yield return null;
        }

        if (enemy) {
            Instantiate(explode, transform);
        }

        Destroy(gameObject, 1.5f);
    }
    #endregion

    #region public
    public void Fire(Enemy enemy) {
        targetPointOffset = enemy.transform.position - Utils.GetPointInCollider(enemy.gameObject);
        StartCoroutine(MoveTo(enemy));
    }
    #endregion
}
