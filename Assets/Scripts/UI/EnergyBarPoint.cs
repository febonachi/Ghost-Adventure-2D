using UnityEngine;
using UnityEngine.UI;

public class EnergyBarPoint : MonoBehaviour {
    public Image image;

    #region private
    private Color EnergyToColor(float energy) {
        Color color = image.color;
        color.a = Utils.TakePercent(1f, energy);
        return color;
    }

    private void Animate() {
        
    }
    #endregion

    #region public
    public void SetEnergy(float energy) {
        if (image.color.a == 0f) Animate();
        image.color = EnergyToColor(energy);
    }
    #endregion
}
