using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SalleDeJeu
{
    public class ObjectActionnable : MonoBehaviour
    {

        [SerializeField] protected bool _isActivated = false;
        [SerializeField] protected logiqueDesSalles scriptSalleAMettreAJour;

        // Start is called before the first frame update
        void Start()
        {
            scriptSalleAMettreAJour = GameObject.FindWithTag("salleDeJeu").GetComponent<logiqueDesSalles>();
            
        }



        public bool Get_isActivated()
        {
            return _isActivated;
        }

    }
}