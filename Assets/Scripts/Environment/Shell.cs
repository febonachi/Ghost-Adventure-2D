using System.Threading.Tasks;

public class Shell : InteractiveEntity {
    #region protected
    protected override async void Interact() {
        base.Interact();

        interactWithMouse = interactWithPlayer = false;

        await Task.Delay(500);

        ui.mainGameUI.shellCount.AddShell();

        Destroy(gameObject);
    }
    #endregion
}
