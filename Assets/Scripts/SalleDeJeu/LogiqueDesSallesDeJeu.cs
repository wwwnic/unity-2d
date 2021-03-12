using System.Collections.Generic;
using UnityEngine;


namespace SalleDeJeu
{
    ///<summary>
    ///Cette classe permet de resoudre l'algebre de boole utilise dans le jeu.
    ///Elle est parente de toutes les salles du jeu, elle fournit aux enfants les outils necessaires pour operer (Liste de levier, operation booleen, etc) 
    ///</summary>
    public abstract class LogiqueDesSallesDeJeu : MonoBehaviour, IlogiqueDesSalles
    {
        [Tooltip("La liste d'objet actionnable, levier / pedestal")]
        [SerializeField] protected List<ObjetActionnable> objectActionnableList;
        [Tooltip("Nom du parametre a modifier dans l'Animator")]
        [SerializeField] protected string objectToActivateParameterName = "activated";
        [Tooltip("La porte a ouvrir")]
        [SerializeField] protected GameObject objectToActivate;


        public List<ObjetActionnable> GetObjetActionnableList()
        {
            return objectActionnableList;
        }

        ///<summary>
        ///Une enumeration des operations possible, 'no', 'true' et 'false' sont exlcus
        ///</summary>
        protected enum OperationBooleen
        {
            AND, OR, NAND, NOR, XOR, XNOR
        }

        ///<summary>
        /// Est appele par un objet actionnale (ex: un levier) quand la veleur booleen de celui-ci est modifie.
        ///</summary>
        public abstract void DetectionChangementObjetActionnable();
        /// <summary>
        /// Fait les operations d'une salle de jeu et retourne si elle est complete
        /// </summary>
        /// <returns></returns>
        public abstract bool CalculeBooleen();


        ///<summary>
        ///Change la condition d'animation d'un Gameobject (ex: ouvre une porte en modifiant son paramettre dans l'animator).
        ///</summary>
        ///<param name= "condition"> valeur booleen qui dicte si l'animation est a true ou false </param>
        public void changerParametreDansAnimator(bool condition)
        {
            objectToActivate.GetComponent<Animator>().SetBool(objectToActivateParameterName, condition);
        }

        /// <summary>
        /// La fonction qui s'occupe de resoudre les operations booleen
        /// </summary>
        /// <param name="operation">le type d'opration</param>
        /// <param name="x">un booleen</param>
        /// <param name="y">un deuxieme booleen</param>
        /// <returns>Un boolean qui correspont a la reponse de la comparaison</returns>
        protected bool ComparateurBooleen(OperationBooleen operation, bool x, bool y)
        {
            switch (operation)
            {
                case OperationBooleen.AND:
                    return x && y;
                case OperationBooleen.OR:
                    return x || y;
                case OperationBooleen.NAND:
                    return !(x && y);
                case OperationBooleen.NOR:
                    return !(x || y);
                case OperationBooleen.XOR:
                    return x ^ y;
                case OperationBooleen.XNOR:
                    return !(x ^ y);
                default:
                    return false;
            }
        }

        /// <summary>
        /// La fonction qui s'occupe de resoudre les operations booleen en passant des ObjectActionnable en parametre
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="objectActionnableX">un objet actionnable</param>
        /// <param name="objectActionnableY">un objet actionnable</param>
        /// <returns>Un boolean qui correspont a la reponse de la comparaison</returns>
        protected bool ComparateurBooleen(OperationBooleen operation, ObjetActionnable objectActionnableX, ObjetActionnable objectActionnableY)
        {
            return ComparateurBooleen(operation, objectActionnableX.GetEstActive(), objectActionnableY.GetEstActive());
        }

    }
}
