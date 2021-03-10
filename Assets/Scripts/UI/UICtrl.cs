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
    private void Awake()
    {
        _canvaInfoJoueur = GameObject.Find("InformationJoueur").GetComponent<CanvasGroup>();
        _cam = Camera.main;
        Time.timeScale = 1;
        bgUI.transform.position = new Vector2(-25.6f, -14.6f);
    }


    /// <summary>
    /// Affiche une série d'objet qui compose un ecran.
    /// </summary>
    /// <param name="ecranAMontrer"></param>
    private void MontrerUnEcran(GameObject[] ecranAMontrer)
    {
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
    public void MontrerEcranVictoire() => MontrerUnEcran(ecranPartieGagnee);

    /// <summary>
    /// Montre l'ecran de defaite.
    /// </summary>
    public void MontrerEcranDefaite() => MontrerUnEcran(ecranPartiePerdu);

    /// <summary>
    /// Affiche le menu pause
    /// </summary>
    /// <param name="menuAffiché">la valeur d'affichage imposé</param>
    public void AfficherMenuPause(bool menuAffiché)
    {

        menuPause.SetActive(menuAffiché);
        ChangerOpaciteInfoJoueur(menuAffiché);
        _joueurAfficheMenuPause = menuAffiché;
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
