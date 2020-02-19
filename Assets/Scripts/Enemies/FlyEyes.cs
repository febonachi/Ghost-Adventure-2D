using System.Linq;
using System.Collections;
using UnityEngine;

public class FlyEyes : MonoBehaviour {

    private SpriteRenderer[] eyes;

    #region private
    private void Start() {
        eyes = GetComponentsInChildren<SpriteRenderer>();
    }

    private IEnumerator ChangeColor(Color color) {
        float elapsed = 0f;
        while(elapsed < 1f) {
            foreach(SpriteRenderer eye in eyes) {
                eye.color = Color.Lerp(eye.color, color, elapsed);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
    #endregion

    #region public
    public void SetColor(Color color) {
        StartCoroutine(ChangeColor(color));
    }
    #endregion
}
