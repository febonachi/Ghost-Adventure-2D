using System;
using UnityEngine;
using System.Threading.Tasks;

[RequireComponent(typeof(Collider2D))]
public class InteractiveEntity : MonoBehaviour {
    #region editor
    [SerializeField] private bool withIdleAnimation = false;
    [SerializeField] private bool withAnimation = false;
    [SerializeField] protected bool interactWithMouse = true;
    [SerializeField] protected bool interactWithPlayer = true;
    [SerializeField] private TwoStateFloat maxStartIdleAnimationDelay;
    #endregion

    protected UIController ui;
    protected Animator animator;
    protected PlayerController player;

    private const string InteractParameter = "Interact";
    private const string CanAnimateParameter = "CanAnimate";

    private bool colliding = false;

    #region private
    private void Start() => Initialize();

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!interactWithPlayer || colliding) return;
        if (collision.CompareTag("Player")) {
            colliding = true;
            Interact();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (!interactWithPlayer || !colliding) return;
        colliding = false;
    }
    #endregion

    #region protected
    protected void Animate() {
        if (withAnimation) animator.SetTrigger(InteractParameter);
    }

    protected virtual async void Initialize() {
        ui = UIController.instance;
        player = PlayerController.instance;

        GetComponent<Collider2D>().isTrigger = true;

        if (withAnimation || withIdleAnimation) {
            animator = GetComponent<Animator>();
        }

        if (withIdleAnimation) {
            await Task.Delay(TimeSpan.FromSeconds(Utils.Random(maxStartIdleAnimationDelay.first, maxStartIdleAnimationDelay.second)));
            if(animator) animator.SetBool(CanAnimateParameter, true);
        }
    }

    protected virtual void OnMouseDown() {
        if (!interactWithMouse) return;
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= player.lightningArea.Radius) Interact();
    }

    protected virtual void Interact() {
        Animate();
    }
    #endregion

    #region public
    public void OnLanternButtonPressed() => Interact();
    #endregion
}
