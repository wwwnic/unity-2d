using UnityEngine;

public class UICtrl : MonoBehaviour
{
    [SerializeField] private GameObject[] ecranPartieGagnee;
    [SerializeField] private GameObject[] ecranPartiePerdu;
    [SerializeField] private Grid bgUI;
    [SerializeField] private GameObject menuPause;

    private Camera _cam;
    private CanvasGroup _canvaInfoJoueur;
    private bool _joueurAfficheMenuPause = false;
    private bool _afficheUnEcranFinal = false;
    private void Awake()
    {
        _canvaInfoJoueur = GameObject.Find("Information joueur").GetComponent<CanvasGroup>();
        _cam = Camera.main;
        Time.timeScale = 1;
        bgUI.transform.position = new Vector2(-25.6f, -14.6f);
    }


    /// <summary>
    /// Affiche une série d'objet qui compose un ecran lorsque la partie est terminee (gagne ou perdu)
    /// </summary>
    /// <param name="ecranAMontrer"></param>
    private void MontrerUnEcranDePartieTerminee(GameObject[] ecranAMontrer)
    {
        _afficheUnEcranFinal = true;
        foreach (GameObject g in ecranAMontrer)
        {
            g.SetActive(true);
            Time.timeScale = 0;
        }
        _cam.orthographicSize = 6;
        Vector2 _positionCamera = _cam.transform.position;
        bgUI.transform.position = _positionCamera;

    }

    /// <summary>
    /// Montre l'ecran de victoire
    /// </summary>
    public void MontrerEcranVictoire() => MontrerUnEcranDePartieTerminee(ecranPartieGagnee);

    /// <summary>
    /// Montre l'ecran de defaite.
    /// </summary>
    public void MontrerEcranPerdu() => MontrerUnEcranDePartieTerminee(ecranPartiePerdu);

    /// <summary>
    /// Affiche le menu pause sauf si la partie est terminee
    /// </summary>
    /// <param name="veutAfficherMenu">Si le joueur souhaite faire pause ou redemarer</param>
    public void AfficherMenuPause(bool veutAfficherMenu)
    {
        if (_afficheUnEcranFinal) return;
        menuPause.SetActive(veutAfficherMenu);
        ChangerOpaciteInfoJoueur(veutAfficherMenu);
        _joueurAfficheMenuPause = veutAfficherMenu;
        Time.timeScale = veutAfficherMenu ? 0 : 1;
    }

    public bool getJoueurAfficheMenuPause() => _joueurAfficheMenuPause;


    /// <summary>
    /// Change l'opacite des infos du joueur (ex: coeur, force)
    /// </summary>
    /// <param name="ChangerOpacite">Si l'opacite doit etre modifie</param>
    private void ChangerOpaciteInfoJoueur(bool ChangerOpacite)
    {
        float nouvelOpaciter = ChangerOpacite ? 0.3f : 1;
        _canvaInfoJoueur.alpha = nouvelOpaciter;

    }

}
