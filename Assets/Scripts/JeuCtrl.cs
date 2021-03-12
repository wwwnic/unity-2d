using UnityEngine;

/// <summary>
/// Contrôleur de jeu.
/// </summary>
public class JeuCtrl : MonoBehaviour
{
    private PersoCtrl _persoCtrl;
    private TailleCameraCtrl _cameraSizeCtrl;
    private bool _enAttaque = false;
    private bool _menuPauseEstOuvert = false;
    private UICtrl _uictrl;

    void Awake()
    {
        _uictrl = GameObject.FindWithTag("ui").GetComponent<UICtrl>();
        _persoCtrl = GameObject.FindWithTag("Player").GetComponent<PersoCtrl>();
        _cameraSizeCtrl = GameObject.FindWithTag("MainCamera").GetComponent<TailleCameraCtrl>();

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
            _persoCtrl.Avancer();
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            _persoCtrl.Reculer();
        }
        else
        {
            _persoCtrl.Arreter();
        }

        // Sauter.
        if (Input.GetAxisRaw("Jump") != 0 || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _persoCtrl.Sauter();
        }
        else
        {
            _persoCtrl.SauterFin();
        }

        // L'attaque.
        _menuPauseEstOuvert = _uictrl.getJoueurAfficheMenuPause();
        if (Input.GetAxisRaw("Fire1") != 0 && !_menuPauseEstOuvert)
        {
            if (!_enAttaque)
            {
                _persoCtrl.Attaquer();
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
           _uictrl.AfficherMenuPause(!_menuPauseEstOuvert);
        }

        //permet le zoom sauf si le joueur regarde la solution
        if (!_menuPauseEstOuvert)
        {
            _cameraSizeCtrl.AjustementZoomCamera(Input.GetAxis("Mouse ScrollWheel"));
        }
    }
}
