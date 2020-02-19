using UnityEngine;

public class EnergyBar : MonoBehaviour {
    public static EnergyBar instance;

    public EnergyBarPoint[] points;

    #region private
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    #endregion

    #region public
    public void UpdateEnergy(float energy) {
        int count = (int)Mathf.Ceil(energy / 10f);
        for (int i = 0; i < points.Length; i++) {
            if(i < count) {
                points[i].SetEnergy(100f);
            }else if(i == count) {
                points[i].SetEnergy((energy % 10) * 10f);
            }else if(i > count) {
                points[i].SetEnergy(0f);
            }
        }
    }
    #endregion
}
