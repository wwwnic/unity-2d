using System.Collections.Generic;
using UnityEngine;
namespace SalleDeJeu
{

    /// <summary>
    /// Classe qui mélange l'etat des objets actionnable des salles passees en serializefield.
    /// </summary>
    public class LevierAleatoireCtrl : MonoBehaviour
    {
        [SerializeField] protected List<LogiqueDesSallesDeJeu> listeDeSalleDeJeu;


        private void Awake()
        {
            ActiverLevierAleatoirement();
        }

        /// <summary>
        /// Actionne les leviers aléatoirement avant le debut de la partie
        /// Modfie l'etat d'une liste d'objet actionnable de facon aleatoire dans une salles qui est dans la liste de salle.
        /// </summary>
        private void ActiverLevierAleatoirement()
        {

            bool salleTerminable;

            foreach (LogiqueDesSallesDeJeu salle in listeDeSalleDeJeu)
            {
                salleTerminable = true;
                ObjetActionnable objectActionnableRetour;
                while (salleTerminable)
                {
                    foreach (ObjetActionnable objectActionnable in salle.GetObjetActionnableList())
                    {
                        objectActionnableRetour = objectActionnable;
                        objectActionnable.SetEstActive(Random.Range(0, 2) > 0);
                    }
                    salleTerminable = salle.CalculeBooleen();
                }
            }
        }

    }
}