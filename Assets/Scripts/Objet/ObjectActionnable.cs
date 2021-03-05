using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SalleDeJeu
{
    public class ObjectActionnable : MonoBehaviour
    {

        [SerializeField] protected bool _isActivated = false;
        [SerializeField] protected LogiqueDesSallesDeJeu scriptSalleAMettreAJour;


        public void Set_isActivated(bool newState)
        {
            _isActivated = newState;
        }


        public bool Get_isActivated()
        {
            return _isActivated;
        }


    }
}