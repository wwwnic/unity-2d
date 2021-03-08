using System.Collections;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Classe qui gère le système de vie du joueur.
/// 
/// Contient les informations du max de vie disponible, du nombre de coeur que le joueur a à chaque instant.
/// 
/// Permet aussi aux ennemi d'infliger des dégâts au joueur.
/// </summary>
public class HealthSystem : MonoBehaviour
{
    [SerializeField] private Image[] coeurs;
    [SerializeField] private Sprite coeurPlein;
    [SerializeField] private Sprite coeurVide;
    [SerializeField] private SpriteRenderer[] knightSprites;
    [SerializeField] private Camera cameraDeDefaite;


    private int _vieDuChevalier = 3;
    private bool _estInvincible;
    private int _vieActuel = 3;

    // Start is called before the first frame update
    void Start()
    {
        if (_vieActuel > _vieDuChevalier)
        {
            _vieActuel = _vieDuChevalier;
        }
        _estInvincible = false;
        MettreAJourLesCoeurs();
    }


    /// <summary>
    /// Met violament fin à la partie. 
    /// </summary>
    private void MettreFinALaPartie()
    {

        if (_vieActuel <= 0)
        {
            cameraDeDefaite.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            Destroy(gameObject);
            GameObject.FindWithTag("ui").GetComponent<UICtrl>().ShowLoseScreen();
        }
    }

    private void MettreAJourLesCoeurs()
    {
        for (int i = 0; i < coeurs.Length; i++)
        {
            if (i < _vieActuel)
            {
                coeurs[i].sprite = coeurPlein;
            }
            else
            {
                coeurs[i].sprite = coeurVide;
            }


            if (i < _vieDuChevalier)
            {
                coeurs[i].enabled = true;
            }
            else
            {
                coeurs[i].enabled = false;
            }
        }

        if (_vieActuel <= 0)
        {
            MettreFinALaPartie();
        }
    }


    /// <summary>
    /// Retire 1 coeur de vie au joueur sauf s’il est actuellement invincible
    /// </summary>
    public void prendreDamageEtDevientInvicibleSaufSiInvincible()
    {
        if (_estInvincible) return;
        prendreDommageEtDevientInvincible();
    }



    /// <summary>
    ///  Retire 1 coeur de vie au joueur 
    /// </summary>
    public void prendreDommageEtDevientInvincible()
    {
        _vieActuel -= 1;
        MettreAJourLesCoeurs();
        StartCoroutine(DevientTemporairementInvincible());
        StartCoroutine(Flash());
    }

    /// <summary>
    /// Permet au joueur de ne pas prendre de dégâts pour un certain temps après s'être fait toucher par un ennemi.
    /// </summary>
    /// <returns>
    /// Retourne un temps d'attente avant de réactiver la fonctionnalité des dégâts au joueur.
    /// </returns>
    private IEnumerator DevientTemporairementInvincible()
    {
        _estInvincible = true;
        yield return new WaitForSeconds(1.5f);
        _estInvincible = false;
    }

    /// <summary>
    /// Permet d'avoir un indicatif visuel lorsque le joueur se fait toucher par un ennemi.
    /// </summary>
    /// <returns>
    /// Retourne l'illusion que le personnage flash à chaque seconde pour un certain temps lorsqu'il se fait toucher par l'ennemi.
    /// </returns>
    private IEnumerator Flash()
    {
        int index = 0;
        while (_estInvincible)
        {
            if (index % 2 != 0)
            {
                foreach (SpriteRenderer s in knightSprites)
                {
                    s.enabled = false;
                }
            }
            else
            {
                foreach (SpriteRenderer s in knightSprites)
                {
                    s.enabled = true;
                }
            }

            index++;
            yield return new WaitForSeconds(0.1f);
        }
        if (!_estInvincible)
        {
            foreach (SpriteRenderer s in knightSprites)
            {
                s.enabled = true;
            }
        }
    }


}
