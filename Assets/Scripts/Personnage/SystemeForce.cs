using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Représente la force du chevalier, il perd 1 force quand il active un levier et en gagne 1 losqu'il réussi à tuer une slime, s'il est a il tombe à 0 force il perd 1 pv
/// </summary>
public class SystemeForce : MonoBehaviour
{

    [SerializeField] private int forceJoueur;
    [SerializeField] private int gainForce;
    [SerializeField] private int perteForce;
    [SerializeField] private SystemeDeVie systemeDeVieAmodifier;
    [SerializeField] private Text texteAmodifier;



    private void Awake()
    {
        texteAmodifier.text = forceJoueur.ToString();

    }

    public int AvoirForceJoueur()
    {
        return forceJoueur;
    }

    /// <summary>
    /// Ajuste la force du joueur et envoi du dégat si la force tombe a -1
    /// </summary>
    /// <param name="ajustement"></param>
    public void MettreJoueurForce(int ajustement)
    {
        forceJoueur += ajustement;
        if (forceJoueur < 0)
        {
            systemeDeVieAmodifier.prendreDommageEtDevientInvincible();
            forceJoueur = 0;
        }
        texteAmodifier.text = forceJoueur.ToString();
    }



}
