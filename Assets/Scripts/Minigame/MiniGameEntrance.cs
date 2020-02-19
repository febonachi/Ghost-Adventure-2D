using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameEntrance : InteractiveEntity {
    #region private
    private async void StartMiniGame() {
        UIController ui = UIController.instance;
        ui.ShowDarkScreen(3f);

        await Task.Delay(2000);

        PlayerController.instance.gameObject.SetActive(false);
        SceneManager.LoadScene("Minigame", LoadSceneMode.Additive);

        ui.HideDarkScreen(3f);
    }
    #endregion

    #region protected
    protected override void Interact() {
        base.Interact();

        if (GameController.instance.HaveMinigameAccess()) {
            StartMiniGame();
        }
    }
    #endregion
}
