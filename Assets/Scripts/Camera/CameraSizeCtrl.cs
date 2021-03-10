using UnityEngine;

/// <summary>
/// Permet de zoomer la caméra avec la roulette de la souris.
/// </summary>
public class CameraSizeCtrl : MonoBehaviour
{
    private Camera cam;
    private float requeteDeZoom;
    [SerializeField] private float vistesseDuZoom = 4f;
    [SerializeField] private float zoomMaxDevant = 6f;
    [SerializeField] private float zoomMaxDerriere = 10f;


    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        requeteDeZoom = cam.orthographicSize;
    }

    // Update is called once per frame
    public void AjustementZoomCamera(float scrollData)
    {
        requeteDeZoom -= scrollData * vistesseDuZoom;
        requeteDeZoom = Mathf.Clamp(requeteDeZoom, zoomMaxDevant, zoomMaxDerriere);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, requeteDeZoom, Time.deltaTime * vistesseDuZoom);
    }
}
