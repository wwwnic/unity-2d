using System.Collections;
using System.Collections.Generic;
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

    private int vie = 3;
    private int nbCoeur = 3;
    private bool _estInvincible;

    [SerializeField] private Image[] coeurs;
    [SerializeField] private Sprite coeurPlein;
    [SerializeField] private Sprite coeurVide;
    [SerializeField] private SpriteRenderer[] knightSprites;

    // Start is called before the first frame update
    void Start()
    {
        _estInvincible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (vie > nbCoeur)
        {
            vie = nbCoeur;
        }

        if (vie <= 0)
        {
            Destroy(gameObject);
            GameObject.FindWithTag("ui").GetComponent<UICtrl>().showLoseScreen();
        }

        for (int i = 0; i < coeurs.Length; i++)
        {
            if (i < vie)
            {
                coeurs[i].sprite = coeurPlein;
            }
            else
            {
                coeurs[i].sprite = coeurVide;
            }
            if (i < nbCoeur)
            {
                coeurs[i].enabled = true;
            }
            else
            {
                coeurs[i].enabled = false;
            }
        }
    }

    /// <summary>
    /// Méthode qui retire 1 coeur de vie au joueur s'il entre en collision avec un ennemi.
    /// </summary>
    public void prendreDamage()
    {
        if (_estInvincible) return;

        vie -= 1;
        _estInvincible = true;
        StartCoroutine(Invulnerable());
        StartCoroutine(Flash());
    }

    /// <summary>
    /// Permet au joueur de ne pas prendre de dégâts pour un certain temps après s'être fait toucher par un ennemi.
    /// </summary>
    /// <returns>
    /// Retourne un temps d'attente avant de réactiver la fonctionnalité des dégâts au joueur.
    /// </returns>
    private IEnumerator Invulnerable()
    {
        yield return new WaitForSeconds(2.0f);
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
