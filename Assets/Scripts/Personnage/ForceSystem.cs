using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Représente la force du chevalier, il perd 1 force quand il active un levier et en gagne 1 losqu'il réussi à tuer une slime, s'il est a il tombe à 0 force il perd 1 pv
/// </summary>
[Serializable]
public class ForceSystem : MonoBehaviour
{

    [SerializeField] private int playerForce;
    [SerializeField] private int winForce;
    [SerializeField] private int loseForce;
    [SerializeField] private HealthSystem healSystemToModify;
    [SerializeField] private Text texteToModify;



    private void Awake()
    {
        texteToModify.text = playerForce.ToString();

    }

    public int GetPlayerForce()
    {
        return playerForce;
    }

    /// <summary>
    /// Ajuste la force du joueur et envoi du dégat si la force tombe a -1
    /// </summary>
    /// <param name="ajustement"></param>
    public void SetPlayerForce(int ajustement)
    {
        playerForce += ajustement;
        if (playerForce < 0)
        {
            healSystemToModify.prendreDommageEtDevientInvincible();
            playerForce = 0;
        }
        texteToModify.text = playerForce.ToString();
    }



}
