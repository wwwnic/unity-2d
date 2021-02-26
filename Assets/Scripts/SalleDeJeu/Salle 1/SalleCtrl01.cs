using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SalleDeJeu
{

    public class SalleCtrl01 : logiqueDesSalles
    {
        [SerializeField] GameObject activatedObject1;


        public override void DetectionChangementObjetActionnable()
        {
            bool test = false;
            bool test2 = false;
            bool rep = false;

            test = CompratateurBoolean(booleanOperation.et_AND, objectActionnableList[0], objectActionnableList[1]);
            test2 = CompratateurBoolean(booleanOperation.ouExclusif_XOR, objectActionnableList[2], objectActionnableList[3]);
            rep = CompratateurBoolean(booleanOperation.et_AND, test, test2);

            if (rep)
            {
                Debug.Log("test = " + rep);
                activatedObject1.GetComponent<Animator>().SetTrigger("activated");
            }
        }

    }
}