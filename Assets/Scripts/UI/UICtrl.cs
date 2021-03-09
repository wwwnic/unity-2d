using UnityEngine;

public class UICtrl : MonoBehaviour
{
    [SerializeField] private GameObject[] ecranPartieGagnee;
    [SerializeField] private GameObject[] ecranPartiePerdu;
    [SerializeField] private Grid bgUI;
    [SerializeField] private GameObject menuPause;

    private GameObject _cam;
    private CanvasGroup _canvaInfoJoueur;
    private Vector2 _positionCamera;

    private void Awake()
    {
        _canvaInfoJoueur = GameObject.Find("InformationJoueur").GetComponent<CanvasGroup>();
        _cam = GameObject.FindGameObjectWithTag("MainCamera");
        Time.timeScale = 1;
        bgUI.transform.position = new Vector2(-25.6f, -14.6f);
    }


    private void Update()
    {
        _positionCamera = _cam.transform.position;
    }

    /// <summary>
    /// Montre l'ecran de victoire
    /// </summary>
    public void MontrerEcranVictoire()
    {
        foreach (GameObject g in ecranPartieGagnee)
        {
            g.SetActive(true);
            Time.timeScale = 0;
        }
        bgUI.transform.position = _positionCamera;
    }


    /// <summary>
    /// Montre l'ecran de defaite.
    /// </summary>
    public void MontrerEcranDefaite()
    {
        foreach (GameObject g in ecranPartiePerdu)
        {
            g.SetActive(true);
        }
        bgUI.transform.position = _positionCamera;
    }

    /// <summary>
    /// Affiche le menu pause
    /// </summary>
    /// <param name="afficherMenu">Si le menu doit etre affiche</param>
    public void AfficherMenuPause(bool afficherMenu)
    {
        menuPause.SetActive(afficherMenu);
        ChangerOpaciteInfoJoueur(afficherMenu);
    }

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
