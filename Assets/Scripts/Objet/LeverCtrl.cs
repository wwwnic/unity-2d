using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace SalleDeJeu
{

    [Serializable]
    public class LeverCtrl : ObjectActionnable
    {
        [SerializeField] SpriteRenderer spriteRendererOn;
        [SerializeField] SpriteRenderer spriteRendererOff;
        [SerializeField] ForceSystem forceSystem;
        private Color _opaciteDemi;
        private Color _opacitePleine;

        private bool _peutEtreActive;

        private void Start()
        {
            RafraichirLevier();
            StartCoroutine(OnTriggerEnter2D(null));
        }

        private void Awake()
        {
            _opaciteDemi = new Color(1f, 1f, 1f, .5f);
            _opacitePleine = new Color(1f, 1f, 1f, 1f);
            _peutEtreActive = true;


        }



        public void RafraichirLevier()
        {
            spriteRendererOn.enabled = _isActivated;
            spriteRendererOff.enabled = !_isActivated;
        }

        private IEnumerator OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "enemy")
            {
                yield return new WaitForSeconds(0.3f);
                spriteRendererOn.color = _opacitePleine;
                spriteRendererOff.color = _opacitePleine;
                _peutEtreActive = true;
            }
        }


        private IEnumerator OnTriggerEnter2D(Collider2D collision)
        {

            if (collision != null)
                if (collision.gameObject.tag == "enemy")
                {
                    _peutEtreActive = false;
                    spriteRendererOn.color = _opaciteDemi;
                    spriteRendererOff.color = _opaciteDemi;

                }
                else if (collision.gameObject.tag == "playerAttackHitbox" && _peutEtreActive)
                {
                    _peutEtreActive = false;
                    spriteRendererOn.enabled = !_isActivated;
                    spriteRendererOff.enabled = _isActivated;
                    _isActivated = !_isActivated;


                    scriptSalleAMettreAJour.DetectionChangementObjetActionnable();
                    forceSystem.SetPlayerForce(-1);
                    yield return new WaitForSeconds(0.5f);
                    _peutEtreActive = true;
                }
        }
    }
}
