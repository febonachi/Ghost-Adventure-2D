using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System.Collections;
using System.Threading.Tasks;

public class LanternButton : MonoBehaviour {
    #region editor
    [SerializeField] private float clickEnergyCost = 15f;
    [SerializeField] private float darknessTime = 6f;
    [SerializeField] private float reloadTime = 4f;
    [SerializeField] private GameObject button;
    [SerializeField] private Image radialImage;
    #endregion

    private Image buttonImage;
    private bool reloading = false;
    private PlayerController player;
    private TypeInfo[] notInteractedTypes;

    #region private
    private void Start() {
        buttonImage = button.GetComponent<Image>();

        notInteractedTypes = new TypeInfo[] { typeof(Item).GetTypeInfo(),
                                              typeof(Lamp).GetTypeInfo(),
                                              typeof(ExitDoor).GetTypeInfo(),
                                              typeof(KeyBlocker).GetTypeInfo(),
                                              typeof(MiniGameEntrance).GetTypeInfo()
        };
    }

    private void OnEnable() {
        player = PlayerController.instance;
    }

    private void OnDisable() {
        buttonImage.color = Color.white;
        radialImage.color = Color.white;
        radialImage.fillAmount = 100f;
        reloading = false;
    }

    private IEnumerator Reload() {
        reloading = true;

        StartCoroutine(Utils.ColorOverLifeTime(radialImage, Color.cyan, 1f));
        StartCoroutine(Utils.ColorOverLifeTime(buttonImage, Utils.transparentWhite, 2f));

        float elapsed = 0f;
        while(elapsed <= 1f) {
            radialImage.fillAmount = 1f - elapsed;
            elapsed += Time.deltaTime / darknessTime;
            yield return null;
        }
        radialImage.fillAmount = 0f;

        radialImage.GetComponent<RectTransform>().Rotate(Vector2.up, 180f);
        Color whiteColorNoAlpha = Color.white;
        elapsed = 0f;
        while(elapsed <= 1f) {
            radialImage.fillAmount = elapsed;
            whiteColorNoAlpha.a = elapsed;
            buttonImage.color = whiteColorNoAlpha;
            elapsed += Time.deltaTime / reloadTime;
            yield return null;
        }
        radialImage.fillAmount = 1f;
        buttonImage.color = Color.white;
        radialImage.GetComponent<RectTransform>().Rotate(Vector2.up, -180f);
        StartCoroutine(Utils.ColorOverLifeTime(radialImage, Color.white, 1f));
        reloading = false;
    }

    private async void InteractWithEntities() {        
        player.AddEnergy(clickEnergyCost, clickEnergyCost / 2f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, player.lightningArea.Radius);
        InteractiveEntity[] entities = colliders.
            Select(e => e.GetComponent<InteractiveEntity>()).
            Where(e => e != null && !notInteractedTypes.Any(type => type.IsAssignableFrom(e.GetType()))).
            OrderBy(e => Vector2.Distance(player.transform.position, e.transform.position)).ToArray();
        foreach (InteractiveEntity entity in entities) {
            entity.OnLanternButtonPressed();
            await Task.Delay(20);
        }
        float prevDecreaseAmount = player.DecreaseEnergyAmount;
        player.DecreaseEnergyAmount = .4f;
        await Task.Delay(2000);
        player.DecreaseEnergyAmount = prevDecreaseAmount;
    }
    #endregion

    #region public
    public void OnLanternButtonCLick() {
        if (!reloading) {
            player.lantern.OnLanternButtonClick();
            InteractWithEntities();
            StartCoroutine(Reload());
        }
    }
    #endregion
}
