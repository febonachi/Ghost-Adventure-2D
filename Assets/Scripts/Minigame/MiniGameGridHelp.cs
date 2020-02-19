using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MiniGameGridHelp : MonoBehaviour {

    public bool IsActive { get { return Progress == 1f; } }
    public bool IsNotActive { get { return Progress == 0f; } }

    private float Progress {
        get {
            return material.GetFloat("_Progress");
        }
        set {
            material.SetFloat("_Progress", value);
        }
    }

    private Material material;
    private SpriteRenderer spriteRenderer;

    #region private
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }

    private IEnumerator ShowSprite() {
        while (Progress < .95f) {
            Progress = Mathf.Lerp(Progress, 1f, Time.deltaTime);
            yield return null;
        }
        Progress = 1f;
    }

    private IEnumerator HideSprite() {
        while (Progress > .05f) {
            Progress = Mathf.Lerp(Progress, 0f, Time.deltaTime * 1.2f);
            yield return null;
        }
        Progress = 0f;
    }
    #endregion

    #region public
    public void SetupSprite(Sprite sprite) {
        spriteRenderer.sprite = sprite;
    }

    public void Show() {
        StartCoroutine(ShowSprite());
    }

    public void Hide() {
        StartCoroutine(HideSprite());
    }

    public void ShowFast() {
        Progress = 1f;
    }

    public void HideFast() {
        Progress = 0f;
    }
    #endregion
}
