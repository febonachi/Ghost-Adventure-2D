using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class MenuUI : MonoBehaviour {
    private UIController ui;

    #region private
    private void Start() {
        ui = UIController.instance;
    }
    #endregion

    #region public
    public void OnPlayButton() {
        ui.LoadScene("Game");
    }

    public void OnGalleryButton() {

    }

    public void OnAboutButton() {

    }

    public async void OnExitButton() {
        ui.ShowDarkScreen(3f);

        await Task.Delay(1000);

        Application.Quit();
    }
    #endregion
}
