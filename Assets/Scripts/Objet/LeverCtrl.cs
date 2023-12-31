﻿using System.Collections;
using UnityEngine;


namespace SalleDeJeu
{
    /// <summary>
    /// La classe qui gere les leviers.
    /// </summary>
    public class LeverCtrl : ObjetActionnable
    {
        [SerializeField] private SpriteRenderer spriteRendererOn;
        [SerializeField] private SpriteRenderer spriteRendererOff;
        [SerializeField] private SystemeDeForce forceSystem;
        [SerializeField] private int CoutForcePourActionner = -1;
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


        /// <summary>
        /// Rafraichit les sprites du levier
        /// </summary>
        private void RafraichirLevier()
        {
            spriteRendererOn.enabled = estActive;
            spriteRendererOff.enabled = !estActive;
        }

        /// <summary>
        /// Permet de retablir les sprites du levier quand un enemie sort du colider du levier.
        /// </summary>
        /// <param name="collision">le colideer qui touche le levier</param>
        /// <returns></returns>
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

        /// <summary>
        /// Permet au joueur d'actionner le levier ou désactive le levier si un slime touche a son colider.
        /// </summary>
        /// <param name="collision">le colideer qui touche le levier</param>
        /// <returns>Le temps restant s'il y a lieu</returns>
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
                    spriteRendererOn.enabled = !estActive;
                    spriteRendererOff.enabled = estActive;
                    estActive = !estActive;


                    scriptSalleAMettreAJour.DetectionChangementObjetActionnable();
                    forceSystem.SetForceJoueur(CoutForcePourActionner);
                    yield return new WaitForSeconds(0.5f);
                    _peutEtreActive = true;
                }
        }
    }
}
