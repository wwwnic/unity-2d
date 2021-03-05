using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SalleDeJeu
{
    public class StatusLevierAleatoire : MonoBehaviour
    {
        [SerializeField] protected List<LogiqueDesSallesDeJeu> listeDeSalleDeJeu;


        private void Start()
        {
            ActiverLevierAleatoirement(true);
        }

        /// <summary>
        /// modfie une l'etat d'une liste d'objet actionnable
        /// </summary>
        /// <param name="objectActionnableList">liste d'objet actionnable</param>
        /// <returns>liste d'objet actionnable</returns>
        private void ActiverLevierAleatoirement(bool executer)
        {
            if (executer)
            {
                bool salleTerminable;

                foreach (LogiqueDesSallesDeJeu salle in listeDeSalleDeJeu)
                {
                    salleTerminable = true;
                    ObjectActionnable objectActionnableRetour;
                    while (salleTerminable)
                    {
                        foreach (ObjectActionnable objectActionnable in salle.Get_objectActionnableList())
                        {
                            objectActionnableRetour = objectActionnable;
                            objectActionnable.Set_isActivated(Random.Range(0, 2) > 0);
                        }
                        salleTerminable = salle.CalculeBooleen();
                    }
                    RafraichirObjetActionnable();
                }
            }
        }

        private void RafraichirObjetActionnable()
        {
            foreach (LogiqueDesSallesDeJeu salle in listeDeSalleDeJeu)
            {
                foreach (ObjectActionnable objectActionnable in salle.Get_objectActionnableList())
                {
                    if (objectActionnable is LeverCtrl)
                    {
                        LeverCtrl levier = (LeverCtrl)objectActionnable;
                        levier.RafraichirLevier();
                    }
                }
            }
        }
    }
}