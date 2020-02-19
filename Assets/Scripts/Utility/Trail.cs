using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {
    #region editor
    public GameObject spawn;
    public int maxCount = 10;
    public float delay = .2f;
    public float fadeTime = .5f;
    #endregion

    public bool Spawn { get; set; } = false;

    private float elapsed = 0f;

    #region private
    private void Update() {
        if (!Spawn) return;
        if (elapsed >= delay) {
            GameObject enqueueObject = Instantiate(spawn, transform.position, transform.rotation);
            StartCoroutine(FadeOut(enqueueObject));
            elapsed = 0f;
        }
        elapsed += Time.deltaTime;
    }

    private IEnumerator FadeOut(GameObject trail) {
        float elapsed = 0f;
        while (elapsed < fadeTime) {
            SpriteRenderer[] sprites = trail.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer sr in sprites) {
                Color color = sr.color;
                color.a -= Time.deltaTime;
                sr.color = color;
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(trail.gameObject);
    }
    #endregion
}
