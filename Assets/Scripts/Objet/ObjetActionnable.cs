using UnityEngine;


namespace SalleDeJeu
{
    /// <summary>
    /// La classe parent des objets actionnable (ex: levier)
    /// </summary>
    public class ObjetActionnable : MonoBehaviour
    {

        [SerializeField] protected bool _isActivated = false;
        [SerializeField] protected LogiqueDesSallesDeJeu scriptSalleAMettreAJour;

        public void SetIsActivated(bool newState)
        {
            _isActivated = newState;
        }

        public bool GetIsActivated()
        {
            return _isActivated;
        }
    }
}