using UnityEngine;


namespace SalleDeJeu
{
    /// <summary>
    /// La classe parent des objets actionnable (ex: levier)
    /// </summary>
    public class ObjetActionnable : MonoBehaviour
    {

        [SerializeField] protected bool estActive = false;
        [SerializeField] protected LogiqueDesSallesDeJeu scriptSalleAMettreAJour;

        public void SetEstActive(bool nouvelEtat)
        {
            estActive = nouvelEtat;
        }

        public bool GetEstActive()
        {
            return estActive;
        }
    }
}