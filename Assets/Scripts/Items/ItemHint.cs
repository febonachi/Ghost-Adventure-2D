using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Threading.Tasks;

[RequireComponent(typeof(Animator))]
public class ItemHint : MonoBehaviour {
    #region editor
    [SerializeField] private GameObject hint;
    [SerializeField] private GameObject question;
    [SerializeField] private GameObject openButton;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private float hideAfterSec = 3f;
    [SerializeField] private UnityEvent onOpenHint;
    #endregion

    private UIController ui;
    private Animator animator;
    private SpriteRenderer hintSpriteRenderer;

    #region private
    private void Awake() {
        ui = UIController.instance;
        animator = GetComponent<Animator>();
        hintSpriteRenderer = hint.GetComponent<SpriteRenderer>();
    }

    private IEnumerator HideAfterSec() {
        float elapsed = 0f;
        while(elapsed < hideAfterSec) {
            elapsed += Time.deltaTime;
            yield return null;
        }
        if(gameObject.activeSelf) Hide();
    }
    #endregion

    #region public
    public void Show() {
        if (gameObject.activeSelf) return;

        gameObject.SetActive(true);
        animator.SetTrigger("Show");

        StartCoroutine(HideAfterSec());
    }

    public async void Hide() {
        if (!gameObject.activeSelf) return;

        animator.SetTrigger("Hide");

        await Task.Delay(500);

        gameObject?.SetActive(false);
    }

    public void OpenHint() {
        onOpenHint.Invoke();
    }

    // Callback по кнопке "Yes" -> у нее меняется аргумент ItemData
    public void OpenHint(ItemData itemData) {
        const int hintCost = 10; // hint cost
        int shellCount = ui.mainGameUI.shellCount.Count();
        if (shellCount < hintCost) {
            return;
        }

        ui.mainGameUI.shellCount.TakeShell(hintCost);

        question.SetActive(false);
        openButton.SetActive(false);
        closeButton.SetActive(false);

        hint.transform.localPosition = itemData.hintHolderPos;
        hint.transform.localScale = new Vector2(itemData.hintHolderSize.width, itemData.hintHolderSize.height);
        hintSpriteRenderer.sprite = itemData.hintSprite;
        hintSpriteRenderer.color = itemData.hintColor;

        hint.SetActive(true);
        StartCoroutine(Utils.RessolveOverLifeTime(hintSpriteRenderer));

        hideAfterSec *= 2f;
    }
    #endregion
}
