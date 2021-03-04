using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Représente la force du chevalier, il perd 1 force quand il active un levier et en gagne 1 losqu'il réussi à tuer une slime, si il est à il tombe à 0 force il perd 
/// </summary>
[Serializable]
public class ForceSystem : MonoBehaviour
{

    [SerializeField] int playerForce;
    [SerializeField] int winForce;
    [SerializeField] int loseForce;
    [SerializeField] HealthSystem healSystemToModify;
    [SerializeField] Text texteToModify;



    private void Awake()
    {
        texteToModify.text = playerForce.ToString();

    }

    public int getPlayerForce()
    {
        return playerForce;
    }

    public void SetPlayerForce(int ajustement)
    {
        playerForce += ajustement;
        if (playerForce < 0)
        {
            healSystemToModify.prendreDamage();
            playerForce = 0;
        }
        texteToModify.text = playerForce.ToString();
    }



}
