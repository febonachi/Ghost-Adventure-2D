using UnityEngine;

public class MiniGameUI : MonoBehaviour {
    private UIController ui;

    #region private
    private void Start() {
        ui = UIController.instance;
    }
    #endregion

    #region public
    public void OnCloseButtonClicked() {
        MiniGame.instance.ExitGame();
    }

    public void OnShuffleButtonClicked() {
        MiniGame.instance.ShuffleGrid();
    }

    public void OnHintButtonClicked() {
        MiniGame.instance.Help();
    }
    #endregion
}
