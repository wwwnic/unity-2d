using UnityEngine;

/// <summary>
/// Contrôleur de jeu.
/// </summary>
public class JeuCtrl : MonoBehaviour
{
    private PersoCtrl persoCtrl;
    private CameraSizeCtrl cameraSizeCtrl;
    private bool _isAttacking = false;
    private bool _regardeLaSolution = false;
    private UICtrl _uictrl;
    // Start is called before the first frame update
    void Awake()
    {
        _uictrl = GameObject.FindWithTag("ui").GetComponent<UICtrl>();
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
        else
        {
            persoCtrl.Arreter();
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
        _regardeLaSolution = _uictrl.getJoueurAfficheMenuPause();
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
            _uictrl.AfficherMenuPause(!_regardeLaSolution);
        }

        //permet le zoom sauf si le joueur regarde la solution
        if (!_regardeLaSolution)
        {
            cameraSizeCtrl.AjustementZoomCamera(Input.GetAxis("Mouse ScrollWheel"));
        }
    }
}
