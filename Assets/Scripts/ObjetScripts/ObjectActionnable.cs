using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SalleDeJeu
{
    public class ObjectActionnable : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRendererOn;
        [SerializeField] SpriteRenderer spriteRendererOff;
        [SerializeField] bool _isActivated = false;
        [SerializeField] logiqueDesSalles scriptSalleAMettreAJour;

        // Start is called before the first frame update
        void Start()
        {
            scriptSalleAMettreAJour = GameObject.FindWithTag("salleDeJeu").GetComponent<logiqueDesSalles>();
            
        }

        // Update is called once per frame
        void Update()
        {

        }


        public bool Get_isActivated()
        {
            return _isActivated;
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