using System.Linq;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {

    [SerializeField]
    private float strength = 1f;
    [SerializeField]
    private Transform[] layers;

    private Vector2 previousPos;
    private Transform playerPosition;

    #region private
    private void Start() {
        playerPosition = PlayerController.instance.transform;
        previousPos = playerPosition.position;
    }

    private void Update() {
        foreach(Transform current in layers) {
            foreach (Transform obj in current) {
                if (Vector2.Distance(obj.position, previousPos) <= 1f) {
                    float x = (previousPos.x - playerPosition.position.x);
                    if (Mathf.Abs(x) >= .015f) {
                        Vector3 targetPos = new Vector3(obj.position.x - x, obj.position.y, obj.position.z);
                        obj.position = Vector3.MoveTowards(obj.position, targetPos, Time.deltaTime * strength * -current.position.z);
                    }
                }
            }
        }
        previousPos = playerPosition.position;
    }
    #endregion
}
