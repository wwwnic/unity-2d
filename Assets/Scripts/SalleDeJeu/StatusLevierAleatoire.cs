using System.Collections.Generic;
using UnityEngine;
namespace SalleDeJeu
{

    /// <summary>
    /// Classe qui mélange l'etat des leviers afin de permettre une rejouabilite
    /// </summary>
    public class StatusLevierAleatoire : MonoBehaviour
    {
        [SerializeField] protected List<LogiqueDesSallesDeJeu> listeDeSalleDeJeu;


        private void Awake()
        {
            ActiverLevierAleatoirement();
        }

        /// <summary>
        /// Modfie l'etat d'une liste d'objet actionnable aleatoirement.
        /// Actionne les leviers aléatoirement avant le début de la partie
        /// </summary>
        /// <param name="objectActionnableList">liste d'objet actionnable</param>
        /// <returns>liste d'objet actionnable</returns>
        private void ActiverLevierAleatoirement()
        {

            bool salleTerminable;

            foreach (LogiqueDesSallesDeJeu salle in listeDeSalleDeJeu)
            {
                salleTerminable = true;
                ObjetActionnable objectActionnableRetour;
                while (salleTerminable)
                {
                    foreach (ObjetActionnable objectActionnable in salle.GetObjectActionnableList())
                    {
                        objectActionnableRetour = objectActionnable;
                        objectActionnable.MettreEstActivé(Random.Range(0, 2) > 0);
                    }
                    salleTerminable = salle.CalculeBooleen();
                }
            }
        }

    }
}