using UnityEngine;

/// <summary>
/// Contrôleur de jeu.
/// </summary>
public class JeuCtrl : MonoBehaviour
{
    private PersoCtrl persoCtrl;
    private TailleCameraCtrl cameraSizeCtrl;
    private bool _enAttaque = false;
    private bool _regardeLaSolution = false;
    private UICtrl _uictrl;
    // Start is called before the first frame update
    void Awake()
    {
        _uictrl = GameObject.FindWithTag("ui").GetComponent<UICtrl>();
        persoCtrl = GameObject.FindWithTag("Player").GetComponent<PersoCtrl>();
        cameraSizeCtrl = GameObject.FindWithTag("MainCamera").GetComponent<TailleCameraCtrl>();

        #if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
        #else
        Debug.logger.logEnabled = false;
        #endif

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
            if (!_enAttaque)
            {
                persoCtrl.Attaquer();
                _enAttaque = true;
            }
        }
        else
        {
            _enAttaque = false;
        }

        // Ouvre ou ferme la solution
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
        {
            _regardeLaSolution = _uictrl.getJoueurAfficheMenuPause();
            if (!_regardeLaSolution)
            {
                _uictrl.AfficherMenuPause(true);
                Time.timeScale = 0;
            }
            if (_regardeLaSolution)
            {
                _uictrl.AfficherMenuPause(false);
                Time.timeScale = 1;
            }
        }

        //permet le zoom sauf si le joueur regarde la solution
        if (!_regardeLaSolution)
        {
            cameraSizeCtrl.AjustementZoomCamera(Input.GetAxis("Mouse ScrollWheel"));
        }
    }
}
