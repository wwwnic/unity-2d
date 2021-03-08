﻿using UnityEngine;

/// <summary>
/// Permet de zoomer la caméra avec la roulette de la souris.
/// </summary>
public class CameraSizeCtrl : MonoBehaviour
{
    private Camera cam;
    private float zoomRequest;
    [SerializeField] private float zoomSpeed = 4f;
    [SerializeField] private float zoomMaxIn = 6f;
    [SerializeField] private float zoomMaxOut = 10f;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        zoomRequest = cam.orthographicSize;
    }

    // Update is called once per frame
    public void AjustementZoomCamera(float scrollData)
    {
        zoomRequest -= scrollData * zoomSpeed;
        zoomRequest = Mathf.Clamp(zoomRequest, zoomMaxIn, zoomMaxOut);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomRequest, Time.deltaTime * zoomSpeed);
    }
}
