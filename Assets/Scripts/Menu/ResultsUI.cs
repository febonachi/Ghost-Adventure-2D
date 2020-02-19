using TMPro;
using UnityEngine;

public class ResultsUI : MonoBehaviour {
    #region editor
    public TextMeshProUGUI energyCount;
    public TextMeshProUGUI shellCount;
    public TextMeshProUGUI enemyCount;
    public TextMeshProUGUI totalScore;
    public GameObject closeButton;
    #endregion

    #region private
    private void Start() {
        closeButton.SetActive(false);
    }
    #endregion

    #region public
    public void OnCloseButtonClicked() {
        UIController.instance.LoadScene("Menu");
    }
    #endregion
}
