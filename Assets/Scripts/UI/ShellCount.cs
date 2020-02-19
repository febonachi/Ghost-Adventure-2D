using TMPro;
using UnityEngine;

public class ShellCount : MonoBehaviour {
    #region editor
    [SerializeField] private TextMeshProUGUI count;
    #endregion   

    private int shellCount = 0;
    private GameScore score;

    #region private
    private void Start() {
        score = GameController.instance.Score;
    }
    #endregion

    #region public
    public void AddShell(int add = 1) {
        shellCount += add;
        count.SetText(shellCount.ToString());
        score.AddShell(1);
    }

    public void TakeShell(int take = 1) {
        shellCount -= take;
        count.SetText(shellCount.ToString());
    }

    public int Count() {
        return shellCount;
    }
    #endregion
}
