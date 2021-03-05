using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SalleDeJeu
{
    public class StatusLevierAleatoire : MonoBehaviour
    {
        [SerializeField] protected List<LogiqueDesSallesDeJeu> listeDeSalleDeJeu;


        private void Awake()
        {
            ActiverLevierAleatoirement(true);
        }

        /// <summary>
        /// Modfie une l'etat d'une liste d'objet actionnable aleatoirement.
        /// </summary>
        /// <param name="objectActionnableList">liste d'objet actionnable</param>
        /// <returns>liste d'objet actionnable</returns>
        private void ActiverLevierAleatoirement(bool executer)
        {
            if (!executer) return;

                bool salleTerminable;

                foreach (LogiqueDesSallesDeJeu salle in listeDeSalleDeJeu)
                {
                    salleTerminable = true;
                    ObjectActionnable objectActionnableRetour;
                    while (salleTerminable)
                    {
                        foreach (ObjectActionnable objectActionnable in salle.GetObjectActionnableList())
                        {
                            objectActionnableRetour = objectActionnable;
                            objectActionnable.SetIsActivated(Random.Range(0, 2) > 0);
                        }
                        salleTerminable = salle.CalculeBooleen();
                    }
                }
        }

    }
}