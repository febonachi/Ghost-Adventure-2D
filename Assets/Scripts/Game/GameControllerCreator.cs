using UnityEngine;

public class GameControllerCreator : MonoBehaviour {
    #region editor
    [SerializeField] private GameObject gameController;
    #endregion

    #region private
    private void Awake() {
        GameController gc = FindObjectOfType<GameController>();
        if (gc == null) {
            Instantiate(gameController);
        }
    }
    #endregion
}
