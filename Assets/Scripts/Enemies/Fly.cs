using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

using static UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class Fly : Enemy {
    #region editor
    [SerializeField] private Vector2 area;
    #endregion

    private FlyEyes eyes;
    private Vector2 startPoint;
    private Vector2 pointToMove;
    private bool inIdle = false;
    private bool inAttack = false;

    #region private
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(area.x, area.y, 0));
    }

    private void Update() {
        if (inAttack || inIdle) return;
        if ((Vector2)transform.position != pointToMove) {
            transform.position = Vector2.MoveTowards(transform.position, pointToMove, Time.deltaTime * moveSpeed);
        } else if (!stunned){            
            bool playerIsLowerThenFly = player.transform.position.y <= transform.position.y;
            if (playerIsLowerThenFly) { 
                float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
                if (distanceToPlayer <= player.lightningArea.Radius) {
                    StartCoroutine(Attack());
                }
            }
            StartCoroutine(GetRandomPoint());
        }
    }

    private IEnumerator GetRandomPoint() {
        inIdle = true;
        yield return new WaitForSeconds(2);
        pointToMove = startPoint - new Vector2(Range(-area.x, area.x) / 2f, Range(-area.y, area.y) / 2f);
        inIdle = false;
    }
    #endregion

    #region protected
    protected override void Initialize() {
        base.Initialize();

        eyes = GetComponentInChildren<FlyEyes>();
        startPoint = transform.position;
        pointToMove = startPoint;
    }

    protected override IEnumerator Attack() {
        inAttack = true;
        eyes.SetColor(Color.red);

        Task task = ShowAttention();

        yield return new WaitUntil(() => task.IsCompleted || task.IsFaulted || task.IsCanceled);

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (task.IsCompleted) {
            if (distanceToPlayer > player.lightningArea.Radius || stunned) {
                HideAttention();
                eyes.SetColor(Color.white);
                inAttack = false;
                yield break;
            }
        }

        animator.SetBool("Stretched", true);
        Vector2 lastPosition = transform.position;
        while(distanceToPlayer >= .1f) {
            Utils.RotateTo(transform, player.transform);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * moveSpeed * 10);
            distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            yield return null;
        }

        HideAttention();

        eyes.SetColor(Color.white);
        player.TakeDamage(attackStrength);
        GameController.instance.ShakeOnce();
        animator.SetBool("Stretched", false);
        while((Vector2)transform.position != lastPosition) {
            transform.position = Vector2.MoveTowards(transform.position, lastPosition, Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * moveSpeed);
            yield return null;
        }
        transform.rotation = Quaternion.identity;
        inAttack = false;
    }
    #endregion
}
