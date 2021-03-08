using UnityEngine;

/// <summary>
/// Contrôleur de jeu.
/// </summary>
public class JeuCtrl : MonoBehaviour
{
    private PersoCtrl persoCtrl;

    [SerializeField] private GameObject solution;

    private CameraSizeCtrl cameraSizeCtrl;
    private bool _isAttacking = false;
    private bool _regardeLaSolution = false;
    // Start is called before the first frame update
    void Start()
    {
        persoCtrl = GameObject.FindWithTag("Player").GetComponent<PersoCtrl>();
        cameraSizeCtrl = GameObject.FindWithTag("MainCamera").GetComponent<CameraSizeCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        // Mouvement gauche/droite.
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            persoCtrl.Avancer();
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            persoCtrl.Reculer();
        }

        // Sauter.
        if (Input.GetAxisRaw("Jump") != 0)
        {
            persoCtrl.Sauter();
        }
        else
        {
            persoCtrl.SauterFin();
        }

        // L'attaque.
        if (Input.GetAxisRaw("Fire1") != 0 && !_regardeLaSolution)
        {
            if (!_isAttacking)
            {
                persoCtrl.Attaquer();
                _isAttacking = true;
            }
        }
        else
        {
            _isAttacking = false;
        }

        // Ouvre ou ferme la solution
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
        {
            _regardeLaSolution = !_regardeLaSolution;
            solution.SetActive(_regardeLaSolution);
        }

        //empeche de zoomer durant la solution
        if (!_regardeLaSolution)
        {
            cameraSizeCtrl.AjustementZoomCamera(Input.GetAxis("Mouse ScrollWheel"));
        }
    }
}
