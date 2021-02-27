using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SalleDeJeu
{


    public class ObjetModifiable : MonoBehaviour
    {
        [SerializeField] GameObject activatedObject1;


        public void ActionAFaireQuandSalleEstTerminee()
        {


         activatedObject1.GetComponent<Animator>().SetTrigger("activated");


        }

        

    }
}