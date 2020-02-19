using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour {
    public static CameraController instance;

    #region editor
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CinemachineVirtualCamera cmVirtualCamera;
    #endregion

    #region private
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(instance.gameObject);
        }
    }
    #endregion
}
