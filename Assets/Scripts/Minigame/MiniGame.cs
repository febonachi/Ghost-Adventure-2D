using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MiniGamePictures {
    public Sprite hint;
    public Sprite[] sprites;
}

[RequireComponent(typeof(Animator))]
public class MiniGame : MonoBehaviour {

    public static MiniGame instance;

    #region editor
    [SerializeField] private MiniGameGrid grid;
    [SerializeField] private MiniGamePictures[] pictures;
    [SerializeField] [Range(0, 10)] private int difficult;
    #endregion

    public int Difficult { get => difficult; }

    private Animator animator;
    private UIController uiController;
    private GameController gameController;
    private const float transitionTime = 2f;
    private bool gameEnded = false;

    #region private
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        animator = GetComponent<Animator>();
        uiController = UIController.instance;
        gameController = GameController.instance;

        StartGame();
    }
    #endregion

    //TODO
    #region public
    public void StartGame() {
        grid.SetupMiniGameInstance(this);
        grid.SetPictures(pictures[0]); // pictures[LVL ID]
        grid.gridHelpHint.HideFast();
        grid.ResetGrid();
    }

    public void StopGame() {
        gameEnded = true;
        animator.SetTrigger("EndGame");
        gameController.SetMinigameAccess(false);
        gameController.Score.MiniGamePassed = true;
    }

    public async void ExitGame() {
        uiController.ShowDarkScreen(transitionTime);

        await Task.Delay(TimeSpan.FromSeconds(transitionTime));
        CameraController.instance.gameObject.SetActive(true);
        PlayerController.instance.gameObject.SetActive(true);

        uiController.miniGameUI.gameObject.SetActive(false);
        uiController.mainGameUI.gameObject.SetActive(true);

        uiController.HideDarkScreen(transitionTime);

        SceneManager.UnloadSceneAsync("Minigame");
    }

    public void ShuffleGrid() {
        if (gameEnded) return;
        grid.ResetGrid();
    }

    public void Help() {
        if (gameEnded) return;
        grid.Help();
    }

    public void SwapAnimation(int block) { }
    #endregion
}
