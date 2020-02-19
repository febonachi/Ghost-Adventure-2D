using UnityEngine;
using System.Threading.Tasks;

public class Item : InteractiveEntity {
    #region editor
    [SerializeField] private ItemData data;
    [SerializeField] private ItemHint hint;
    [SerializeField] protected GameObject item;
    [SerializeField] protected ParticleSystem itemPs;
    #endregion

    public static ItemOrder ItemId { get; private set; } = ItemOrder.None;

    private bool itemTaken = false;
    private bool canTakeItem = false;
    private bool inInteraction = false;
    private bool canInteractWithItem = true;

    private GameController gameController;
    private SpriteRenderer itemSpriteRenderer;

    #region private
    private bool IsAvailable() {
        return ((int)data.order - (int)ItemId == 1);
    }

    private async void TakeItemBehavior() {
        itemTaken = true;
        canTakeItem = false;
        ItemId = data.order;

        TakeItem(); // virtual

        await Task.Delay(200);

        ui.mainGameUI.itemHolder.SetupItem(data);

        await Task.Delay(1000);

        item.SetActive(false);
    }
    #endregion

    #region protected
    protected override void Initialize() {
        base.Initialize();

        gameController = GameController.instance;
        itemSpriteRenderer = item.GetComponent<SpriteRenderer>();

        hint?.Hide();
    }

    protected override void Interact() {
        if (IsAvailable()) {
            if (canInteractWithItem && !inInteraction && !canTakeItem) InteractWithItem();
            else if(canTakeItem) TakeItemBehavior();
        } else if(!itemTaken) hint?.Show();
    }

    protected virtual void InteractWithItem() {
        hint?.Hide();

        inInteraction = true;

        Animate();
    }

    protected virtual void TakeItem() {
        if(itemPs) itemPs.Stop();
        StartCoroutine(Utils.DissolveOverLifeTime(itemSpriteRenderer));
    }

    protected virtual void InteractionEnded() {
        canTakeItem = true;
        inInteraction = false;
        canInteractWithItem = false;

        if(itemPs) itemPs.Play();
    }
    #endregion

    #region public
    public void OpenHint() {
        hint.OpenHint(data);
    }

    public ItemOrder Order() => data.order;
    #endregion
}
