using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace SalleDeJeu { 

    [Serializable]
    public class LeverCtrl : ObjectActionnable
    {

    [SerializeField] SpriteRenderer spriteRendererOn;
    [SerializeField] SpriteRenderer spriteRendererOff;


        private void Start()
        {
            if (Get_isActivated())
            {
                spriteRendererOn.enabled = true;
                spriteRendererOff.enabled = false;
                _isActivated = true;
            }
            else
            {
                spriteRendererOn.enabled = false;
                spriteRendererOff.enabled = true;
                _isActivated = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.isTrigger)
            {
                if (_isActivated)
                {
                    spriteRendererOn.enabled = false;
                    spriteRendererOff.enabled = true;
                    _isActivated = false;
                }
                else
                {
                    spriteRendererOn.enabled = true;
                    spriteRendererOff.enabled = false;
                    _isActivated = true;
                }
                scriptSalleAMettreAJour.DetectionChangementObjetActionnable();
            }
        }
    }
}