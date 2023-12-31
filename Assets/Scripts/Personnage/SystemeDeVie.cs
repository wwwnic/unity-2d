﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Classe qui gère le système de vie du joueur.
/// 
/// Contient les informations du max de vie disponible, du nombre de coeur que le joueur a à chaque instant.
/// 
/// Permet aussi aux ennemi d'infliger des dégâts au joueur.
/// </summary>
public class SystemeDeVie : MonoBehaviour
{
    [SerializeField] private Image[] coeurs;
    [SerializeField] private Sprite coeurPlein;
    [SerializeField] private Sprite coeurVide;
    [SerializeField] private SpriteRenderer[] knightSprites;
    [SerializeField] private Camera cameraDeDefaite;


    private int _vieDuChevalier = 3;
    private bool _estInvincible;
    private int _vieActuel = 3;
    private UICtrl _uictrl;
    private PersoCtrl _persoCtrl;
    void Awake()
    {
        _persoCtrl = GameObject.FindWithTag("Player").GetComponent<PersoCtrl>();
        _uictrl = GameObject.FindWithTag("ui").GetComponent<UICtrl>();
        _estInvincible = false;
        ChangerLaVieDuChevalier();
    }

    // Start is called before the first frame update
    void Start()
    {
        MettreAJourLesCoeurs();
    }

    /// <summary>
    /// Change la vie du chevlier au debut de la partie si sa vie maximum est plus basse que sa vie actuel.
    /// </summary>
    private void ChangerLaVieDuChevalier()
    {
        if (_vieActuel > _vieDuChevalier)
        {
            _vieActuel = _vieDuChevalier;
        }
    }

    /// <summary>
    /// Met brutalement fin à la partie. 
    /// </summary>
    private void MettreFinALaPartie()
    {
        cameraDeDefaite.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        CacherLesSpritesDuChevalier();
        _persoCtrl.JoueurEstMort();
        _uictrl.MontrerEcranPerdu();
    }

    /// <summary>
    /// Met a jour les coeurs
    /// </summary>
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
                CacherLesSpritesDuChevalier();
            }
            else
            {
                MontrerLesSpriteDuChevalier();
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

    /// <summary>
    /// Cache visuelement les sprites du chevalier
    /// </summary>
    private void CacherLesSpritesDuChevalier()
    {
        foreach (SpriteRenderer s in knightSprites)
        {
            s.enabled = false;
        }
    }
    /// <summary>
    /// Montre visuelement les sprites du chevalier 
    /// </summary>
    private void MontrerLesSpriteDuChevalier()
    {
        foreach (SpriteRenderer s in knightSprites)
        {
            s.enabled = true;
        }
    }
}