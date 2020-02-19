using UnityEngine;
using Cinemachine;

public class LightningArea : MonoBehaviour {
    #region editor
    [SerializeField] private float maxScale;
    [SerializeField] private float maxCameraDistance;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private SpriteRenderer backgroundArea;
    #endregion

    public float Radius { get; private set; }
    public Color LightBackgroundColor {
        get => backgroundArea.color;
        set => ChangeBackgroundColor(value);
    }

    private Vector3 scale;
    private Vector3 startScale;

    private float distance;
    private float startDistance;
    private float backgroundAlpha;

    #region private
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

    private void ChangeBackgroundColor(Color c) {
        StopAllCoroutines();
        Color newColor = c;
        newColor.a = backgroundAlpha;
        backgroundArea.color = newColor;
    }

    private void Start() {
        startScale = transform.localScale;
        backgroundAlpha = LightBackgroundColor.a;
        scale = new Vector3(maxScale - startScale.x, maxScale - startScale.y, startScale.z);

        startDistance = virtualCamera.m_Lens.OrthographicSize;
        distance = maxCameraDistance - startDistance;
    }
    #endregion

    #region public
    public void UpdateEnergy(float energy) {
        float percent = Utils.Percent(energy, 100f);
        float scaleX = startScale.x + Utils.TakePercent(scale.x, percent);
        float scaleY = startScale.y + Utils.TakePercent(scale.y, percent);
        transform.localScale = new Vector3(scaleX, scaleY, startScale.z);

        float cameraDistance = startDistance + Utils.TakePercent(distance, percent);
        virtualCamera.m_Lens.OrthographicSize = cameraDistance;
        Radius = cameraDistance * .9f;
    }
    #endregion
}
