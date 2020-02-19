using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : InteractiveEntity {
    #region editor
    [SerializeField] private GameObject keyHole;
    [SerializeField] private ItemData itemData;
    [SerializeField] private ItemHint itemHint;
    #endregion

    #region private
    private void OnDisable() {
        itemHint?.Hide();
    }

    private void OpenDoor() {
        UIController.instance.LoadScene("Results");
    }
    #endregion

    #region protected
    protected override void Initialize() {
        base.Initialize();

        itemHint.Hide();
    }

    protected override void Interact() {
        //OpenDoor();
        if (Item.ItemId != ItemOrder.Key) {
            itemHint.Show();
        } else {
            OpenDoor();
        }
    }
    #endregion

    #region public
    public void OpenHint() => itemHint.OpenHint(itemData);
    #endregion
}
