using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represente la force du chevalier, il perd 1 force quand il active un levier et en gagne 1 losqu'il réussi à tuer une slime, s'il est a il tombe à 0 force il perd 1 pv
/// </summary>
public class SystemeDeForce : MonoBehaviour
{

    [SerializeField] private int forceDuJoueur;
    [SerializeField] private SystemeDeVie systemeDeVieAmodifier;
    [SerializeField] private Text texteAmodifier;

    private void Awake()
    {
        texteAmodifier.text = forceDuJoueur.ToString();

    }

    /// <summary>
    /// Ajuste la force du joueur et envoi du dégat si la force tombe a -1
    /// </summary>
    /// <param name="ajustement"></param>
    public void SetForceJoueur(int ajustement)
    {
        forceDuJoueur += ajustement;
        if (forceDuJoueur < 0)
        {
            systemeDeVieAmodifier.prendreDommageEtDevientInvincible();
            forceDuJoueur = 0;
        }
        texteAmodifier.text = forceDuJoueur.ToString();
    }
}
