using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{

    private int vie = 3;
    private int nbCoeur = 3;

    [SerializeField] private Image[] coeurs;
    [SerializeField] private Sprite coeurPlein;
    [SerializeField] private Sprite coeurVide;

    // Start is called before the first frame update
    void Start()
    {
        
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
        }

        for (int i = 0; i < coeurs.Length; i++)
        {
            if (i < vie)
            {
                coeurs[i].sprite = coeurPlein;
            } else
            {
                coeurs[i].sprite = coeurVide;
            }
            if (i < nbCoeur)
            {
                coeurs[i].enabled = true;
            } else
            {
                coeurs[i].enabled = false;
            }
        }
    }

    public void prendreDamage()
    {
        vie -= 1;
    }
}
