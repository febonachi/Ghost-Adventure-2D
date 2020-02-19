using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class ChancePrefab {
    public GameObject shell;
    public float chance;
}

public class ChanceSpawner : MonoBehaviour {
    #region editor
    [SerializeField] ChancePrefab[] prefabs;
    #endregion

    #region public
    public async void Spawn() {
        ChancePrefab prefab = prefabs[Utils.Random(0, prefabs.Length)];
        if (prefab == null) return;

        if (Utils.Random(0f, 100f) <= prefab.chance) {
            await Task.Delay(500);
            Instantiate(prefab.shell, transform.position, Quaternion.identity);
        }
    }
    #endregion
}
