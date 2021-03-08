using UnityEngine;

/// <summary>
/// Script de contrôle de la caméra.
/// </summary>
public class CameraCtrl : MonoBehaviour
{
    private Rigidbody2D rb;
    private PersoCtrl personnage;
    [SerializeField] float vitesseDeLaCameraEnX = 4.0f;
    [SerializeField] float vitesseDeLaCameraEnY = 10.0f;
    [SerializeField] float hauteurDeLaCamera = 2.5f;


    // Start is called before the first frame update
    void Start()
    {
        personnage = GameObject.FindWithTag("Player").GetComponent<PersoCtrl>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 posPersonage = personnage.transform.position;
        Vector2 posCamera = transform.position;
        float distanceEntrePersonnageEtCameraEnX = posPersonage.x - posCamera.x;
        float distanceEntrePersonnageEtCameraEnY = posPersonage.y - posCamera.y + hauteurDeLaCamera;

        rb.velocity = new Vector2(distanceEntrePersonnageEtCameraEnX * vitesseDeLaCameraEnX, distanceEntrePersonnageEtCameraEnY * vitesseDeLaCameraEnY);
    }

}
