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
    [SerializeField] ForceSystem forceSystem;

        private bool _peutEtreActive;

        private void Start()
        {
            StartCoroutine(OnTriggerEnter2D(null));
        }

        private void Awake()
        {
            _peutEtreActive = true;
            if (_isActivated)
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

        private IEnumerator OnTriggerEnter2D(Collider2D other)
        {
            if (other != null && other.isTrigger && _peutEtreActive)
            {
                if (_isActivated)
                {
                    _peutEtreActive = false;
                    spriteRendererOn.enabled = false;
                    spriteRendererOff.enabled = true;
                    _isActivated = false;
                }
                else
                {
                    _peutEtreActive = false;
                    spriteRendererOn.enabled = true;
                    spriteRendererOff.enabled = false;
                    _isActivated = true;
                }
                scriptSalleAMettreAJour.DetectionChangementObjetActionnable();
                forceSystem.SetPlayerForce(-1);
                yield return new WaitForSeconds(0.5f);
                _peutEtreActive = true;
            }

        }
    }
}