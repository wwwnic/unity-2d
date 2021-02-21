using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    private Rigidbody2D rb;
    private PersoCtrl personnage;
    [SerializeField] float vitesse = 1.0f;
    // La layer avec laquelle la caméra peut avoir une collision.
    [SerializeField] LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        personnage = GameObject.FindWithTag("Player").GetComponent<PersoCtrl>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posPerso = personnage.transform.position;
        Vector3 posCamera = transform.position;
        float distance = posCamera.x - posPerso.x;

        rb.velocity = new Vector2(-distance * vitesse, 0);
    }
}
