using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : GizmoItem {

    #region editor
    [SerializeField] private float insideSpeed = .1f;
    [SerializeField] private LampEnergy spawn;
    #endregion

    public int EnergyCount => spawnedObjects.Keys.Count;

    private int count = 0;
    private Color color = Color.white;
    private Dictionary<LampEnergy, Vector2> spawnedObjects = new Dictionary<LampEnergy, Vector2>();

    #region private
    private void Update() {
        if (count <= 0) return;
        List<LampEnergy> objects = new List<LampEnergy>(spawnedObjects.Keys);
        foreach (LampEnergy obj in objects) {
            Vector2 currentPos = obj.transform.localPosition;
            if (currentPos != spawnedObjects[obj]) {
                obj.transform.localPosition = Vector2.MoveTowards(currentPos, spawnedObjects[obj], Time.deltaTime * insideSpeed);
            } else {
                spawnedObjects[obj] = GetNewPosition();
            }
        }
    }

    private Vector2 GetNewPosition() {
        return new Vector2(Random.Range(-visibleRange, visibleRange), Random.Range(-visibleRange, visibleRange));
    }
    #endregion

    #region public
    public void Initialize(int value, Color color) {
        count = value;
        this.color = color;
        spawnedObjects.Clear();
        for(int i = 0; i < value; i++) {
            ReloadOne();
        }
    }

    public bool CanTakeOne() {
        return spawnedObjects.Keys.Count > 0;
    }    

    public LampEnergy TakeOne() {
        if (spawnedObjects.Keys.Count <= 0) return null;
        LampEnergy one = spawnedObjects.Keys.ToList()[Random.Range(0, spawnedObjects.Keys.Count)];
        spawnedObjects.Remove(one);
        return one;
    }

    public void ReloadOne() {
        LampEnergy obj = Instantiate(spawn, transform);
        spawnedObjects.Add(obj, GetNewPosition());
        obj.ChangeColor(color);
    }

    public void Clear() {
        spawnedObjects.Clear();
    }

    public void ClearAndDestroy() {
        while(EnergyCount > 0) {
            Destroy(TakeOne().gameObject);
        }
    }
    #endregion
}
