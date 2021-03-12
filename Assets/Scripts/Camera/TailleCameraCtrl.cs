using UnityEngine;

/// <summary>
/// Permet de zoomer la caméra avec la roulette de la souris.
/// </summary>
public class TailleCameraCtrl : MonoBehaviour
{
    private Camera _cam;
    private float _requeteDeZoom;
    [SerializeField] private float vitesseDuZoom = 4f;
    [SerializeField] private float zoomMaxDevant = 6f;
    [SerializeField] private float zoomMaxDerriere = 10f;


    // Start is called before the first frame update
    private void Awake()
    {
        _cam = Camera.main;
        _requeteDeZoom = _cam.orthographicSize;
    }

    // Ajuste le la grandeur de la camera pour simuler un zoom
    public void AjustementZoomCamera(float valeurRoulette)
    {
        _requeteDeZoom -= valeurRoulette * vitesseDuZoom;
        _requeteDeZoom = Mathf.Clamp(_requeteDeZoom, zoomMaxDevant, zoomMaxDerriere);
        _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, _requeteDeZoom, Time.deltaTime * vitesseDuZoom);
    }
}
