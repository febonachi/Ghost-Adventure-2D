using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;

    public Lantern lantern;
    public GameObject ghost;
    public LightningArea lightningArea;
    public float DecreaseEnergyAmount;

    public float speed = 300f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float maxJumpTime = .5f;
    public float timeBetweenJumps = .2f;

    private Rigidbody2D rb;
    private Joystick joystick;
    private Animator animator;
    private float direction = 0;
    private float jumpTime = 0f;
    private bool canJump = true;
    private bool canMove = true;
    private bool inJump = false;
    private bool flipped = false;
    private bool jumpOnce = true;
    private bool grounded = false;
    private float startGravityScale;
    private float groundRadius = .05f;
    private float timeElapsedAfterJump = 0f;

    [SerializeField]
    [Range(0f, 100f)]
    private float energy = 50f;
    private bool decreaseEnergy = true;
    private float energyToAdd = 0f;
    private GameController gameController;

    public UnityEventFloat energyChanged;

    #region private
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        gameController = GameController.instance;

        energyChanged.AddListener(GameObject.FindObjectOfType<EnergyBar>().UpdateEnergy);

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startGravityScale = rb.gravityScale;

        DecreaseEnergyAmount = Time.deltaTime;
    }

    private void OnEnable() {
        joystick = UIController.instance.mainGameUI.joystick;
    }

    private void Update() {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if (Input.GetKey(KeyCode.E)) {
            UIController.instance.mainGameUI.lanternButton.OnLanternButtonCLick();
        }

        //#if UNITY_EDITOR || UNITY_STANDALONE
        //        direction = Input.GetAxisRaw("Horizontal");

        //        if (Input.GetButton("Jump")) {
        //            StartJump();
        //        }

        //        if (Input.GetButtonUp("Jump")) {
        //            StopJump();
        //        }
        //#elif UNITY_ANDROID
        //        direction = joystick.Horizontal;

        //        if (joystick.Vertical >= .4f) {
        //            StartJump();
        //        }

        //        if (joystick.Vertical <= 0f) {
        //            StopJump();
        //        }
        //#endif

        direction = joystick.Horizontal;

        if (joystick.Vertical >= .4f) {
            StartJump();
        }

        if (joystick.Vertical <= 0f) {
            StopJump();
        }

        /**/

        if (grounded) {
            if(timeElapsedAfterJump < timeBetweenJumps) { 
                timeElapsedAfterJump += Time.deltaTime;
            }
        } else {
            timeElapsedAfterJump = 0f;
        }

        canJump = timeElapsedAfterJump > timeBetweenJumps;

        if (direction < 0 && !flipped) {
            Flip(true);
        } else if (direction > 0 && flipped) {
            Flip(false);
        }

        if (decreaseEnergy && energy > 0) {
            energy -= DecreaseEnergyAmount;
        }

        energyChanged.Invoke(energy);
    }

    private void FixedUpdate () {
        if (!canMove) return;
        float horizontal = direction * speed * Time.deltaTime;

        if (grounded && Mathf.Abs(horizontal) == 0f) {
            rb.gravityScale = startGravityScale / 2f;
        } else {
            rb.gravityScale = startGravityScale;
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        rb.velocity = new Vector2(horizontal, rb.velocity.y);

        Vector3 rotation = transform.rotation.eulerAngles;
        if (horizontal != 0) {
            transform.rotation = Quaternion.Euler(rotation.x, rotation.y, -6f);
        } else {
            transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
        }
    }

    private void Flip(bool state) {
        transform.rotation = Quaternion.Euler(Vector3.up * (180 * (flipped ? 0 : 1)));
        flipped = state;
    }

    private void StartJump() {
        if (grounded && canJump && !inJump) {
            rb.velocity = Vector2.up * jumpForce;
            jumpTime = 0f;
            inJump = true;
        } else if (inJump && !grounded) {
            if (jumpTime > .15f && jumpOnce) {
                animator.SetTrigger("Jump");
                jumpOnce = false;
            }
            if (jumpTime <= maxJumpTime) {
                rb.velocity = Vector2.up * jumpForce;
                jumpTime += Time.deltaTime;
            } else {
                StopJump();
            }
        }
    }

    private void StopJump() {
        inJump = false;
        jumpOnce = true;
    }

    private IEnumerator EnergyCharger(float value) {
        decreaseEnergy = false;
        while(energyToAdd > 0f) {
            energy += value;
            energyToAdd -= Mathf.Abs(value);
            yield return null;
        }
        decreaseEnergy = true;
    }
    #endregion

    #region public
    public bool CanTakeEnergy(float value) {
        if(energy + energyToAdd + value < 105f) {
            return true;
        }
        return false;
    }

    public void AddEnergy(float value, float val = .5f) {
        gameController.Score.AddEnergy(Mathf.Abs((int)value)); // score

        energyToAdd = Mathf.Clamp(energyToAdd + value, 0f, 100f - energy);
        if(decreaseEnergy) StartCoroutine(EnergyCharger(val));
    }

    public void TakeEnergy(float value) {
        energy -= value;
        if (energy < 0) energy = 0f;
    }

    public async void AddForce(float strength) {
        canMove = false;

        int sign = transform.right.x < 0 ? 1 : -1;
        rb.AddForce(new Vector2(.5f * sign, .5f) * strength, ForceMode2D.Impulse);

        await Task.Delay(500);

        canMove = true;
    }

    public void TakeDamage(float count) {
        if (!gameObject.activeSelf) return;
        animator.SetTrigger("Damage");
        energy -= count;
        if (energy < 0) energy = 0f;
    }
    #endregion
}
