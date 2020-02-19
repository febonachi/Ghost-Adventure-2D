using System;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public abstract class Enemy : InteractiveEntity {
    #region editor
    [SerializeField] protected float moveSpeed = 1f;
    [SerializeField] protected float attackStrength = 10f;
    [SerializeField] private float stunTime = 2f;
    [SerializeField] private int stunCount = 3;
    [SerializeField] private ParticleSystem stunPs;
    [SerializeField] private ParticleSystem diePs;
    [SerializeField] private GameObject[] attention;
    #endregion

    private float speed = 0f;
    
    protected bool stunned = false;

    #region protected
    protected override void Initialize() {
        base.Initialize();

        foreach(GameObject sr in attention) {
            sr.SetActive(false);
        }

        speed = moveSpeed;
    }

    protected override void Interact() {
        Stun();
    }

    protected async Task ShowAttention() {
        foreach (GameObject sr in attention) {
            sr.SetActive(true);
            await Task.Delay(300);
        }
    }

    protected void HideAttention() {
        foreach (GameObject sr in attention) {
            sr.SetActive(false);
        }
    }

    private async void Stun() {
        if (stunned) return;
        if (stunCount-- <= 0) {
            Die();
            return;
        }

        stunned = true;
        moveSpeed = .1f;
        animator.SetBool("Stun", stunned);

        Destroy(Instantiate(stunPs, transform).gameObject, stunTime + 3f);
        await Task.Delay(TimeSpan.FromSeconds(stunTime));

        stunned = false;
        moveSpeed = speed;
        animator.SetBool("Stun", stunned);
    }

    private void Die() {
        ParticleSystem ps = Instantiate(diePs, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(ps.gameObject, 3f);
        GameController.instance.Score.AddEnemyKill(1);
    }

    protected abstract IEnumerator Attack();
    #endregion
}
