using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour {
    public static UIController instance;

    #region editor
    public MenuUI menuUI;
    public MainGameUI mainGameUI;
    public MiniGameUI miniGameUI;
    public ResultsUI resultsUI;
    [SerializeField] private Image darkScreen;
    #endregion

    private RectTransform rectTransform;

    #region private
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        rectTransform = GetComponent<RectTransform>();        

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        HideAllUI();
        switch (scene.name) {
            case "Menu": {
                AudioController.instance.Stop("MainGame");
                AudioController.instance.Play("Menu");
                menuUI.gameObject.SetActive(true);
                break;
            }
            case "Game": {
                AudioController.instance.Stop("Menu");
                AudioController.instance.Play("MainGame");
                GameController.instance.ResetScore();
                mainGameUI.gameObject.SetActive(true);
                break;
            }
            case "Minigame": {
                CameraController.instance.gameObject.SetActive(false);
                miniGameUI.gameObject.SetActive(true);
                break;
            }
            case "Results": {
                resultsUI.gameObject.SetActive(true);
                break;
            }
            default: break;
        }
        HideDarkScreen(5f);
    }

    private void HideAllUI() {
        menuUI.gameObject.SetActive(false);
        mainGameUI.gameObject.SetActive(false);
        miniGameUI.gameObject.SetActive(false);
        resultsUI.gameObject.SetActive(false);
        ShowDarkScreen(0f);
    }
    #endregion

    #region public
    public async void LoadScene(string name) {
        ShowDarkScreen(3f);

        await Task.Delay(2000);

        SceneManager.LoadScene(name);

        HideDarkScreen(3f);
    }

    public Vector3 UIToWorldPoint(Vector2 point) {
        Vector3 pos = Vector3.zero;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, point, Camera.main, out pos);
        return pos;
    }

    public void HideDarkScreen(float time = 1f) {
        StopAllCoroutines();
        StartCoroutine(Utils.ColorOverLifeTime(darkScreen, Utils.transparentBlack, time));
    }

    public void ShowDarkScreen(float time = 1f) {
        StopAllCoroutines();
        StartCoroutine(Utils.ColorOverLifeTime(darkScreen, Color.black, time));
    }
    #endregion
}
