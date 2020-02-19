using TMPro;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class Results : MonoBehaviour {
    #region editor
    [SerializeField] private GameObject minigameItem;
    #endregion

    private GameScore score;
    private ResultsUI resultUI;

    #region private
    private void Start() {
        score = GameController.instance.Score;
        resultUI = UIController.instance.resultsUI;

        minigameItem.SetActive(score.MiniGamePassed);

        ShowResults();
    }

    private IEnumerator ShowScore(TextMeshProUGUI field, int value) {
        int delta = 1;
        int current = 0;
        if(value <= 200) {
            delta = 1;
        } else if(value > 200 && value <= 500) {
            delta = 2;
        }else if(value > 500 && value <= 1000) {
            delta = 3;
        }else {
            delta = 4;
        }
        while(current < value) {
            current += delta;
            field.SetText(current.ToString());
            yield return null;
        }
    }

    private async void ShowResults() {
        StartCoroutine(ShowScore(resultUI.energyCount, score.EnergyCount));
        await Task.Delay(500);
        StartCoroutine(ShowScore(resultUI.shellCount, score.ShellCount));
        await Task.Delay(500);
        StartCoroutine(ShowScore(resultUI.enemyCount, score.EnemyCount));
        await Task.Delay(500);
        StartCoroutine(ShowScore(resultUI.totalScore, score.TotalScore()));
        await Task.Delay(1000);
        resultUI.closeButton.SetActive(true);
    }
    #endregion
}
