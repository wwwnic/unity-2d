﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraZoomCtrl : MonoBehaviour
{
    private Camera cam;
    private float zoomRequest;
    [SerializeField] float zoomSpeed = 4f;
    [SerializeField] float zoomMaxIn = 6f;
    [SerializeField] float zoomMaxOut = 10f;



    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        zoomRequest = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        zoomRequest -= scrollData * zoomSpeed;
        zoomRequest = Mathf.Clamp(zoomRequest, zoomMaxIn, zoomMaxOut);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomRequest, Time.deltaTime * zoomSpeed);
    }
}