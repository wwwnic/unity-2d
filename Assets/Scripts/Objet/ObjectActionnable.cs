using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SalleDeJeu
{
    public class ObjectActionnable : MonoBehaviour
    {

        [SerializeField] protected bool _isActivated = false;
        [SerializeField] protected LogiqueDesSalles scriptSalleAMettreAJour;





        public bool Get_isActivated()
        {
            return _isActivated;
        }

    }
}