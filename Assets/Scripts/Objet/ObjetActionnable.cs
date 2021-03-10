using UnityEngine;


namespace SalleDeJeu
{
    /// <summary>
    /// La classe parent des objets actionnable (ex: levier)
    /// </summary>
    public class ObjetActionnable : MonoBehaviour
    {

        [SerializeField] protected bool _estActivé = false;
        [SerializeField] protected LogiqueDesSallesDeJeu scriptSalleAMettreAJour;

        public void MettreEstActivé(bool nouvelÉtat)
        {
            _estActivé = nouvelÉtat;
        }

        public bool AvoirEstActivé()
        {
            return _estActivé;
        }
    }
}