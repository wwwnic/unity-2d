using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SalleDeJeu
{


    ///<summary>
    ///Cette classe permet de resoudre l'algebre de boole utilise dans le jeu.
    ///
    ///Cette classe est parente de toutes les salles du jeu, elle fournit aux enfants les outils necessaires pour operer (Liste de levier, objet a actionner, etc) 
    ///Ensuite, elle résout l'algebre de Boole (voir énum BooleanOperation) 
    ///Elle est surchargée afin d'accommoder le scripting des salles au maximum.
    ///</summary>
    public abstract class LogiqueDesSalles : MonoBehaviour, IlogiqueDesSales
    {
        [Tooltip("La liste d'obejet actionnable, levier / pedestal")]
        [SerializeField] protected List<ObjectActionnable> objectActionnableList;
        [Tooltip("Nom du parametre a modifier dans l'Animator")]
        [SerializeField] protected string objectToActivateParameterName = "activated";
        [Tooltip("La porte a ouvrir")]
        [SerializeField] protected GameObject objectToActivate;



        ///<summary>
        ///Une enumeration des operations possible
        ///</summary>
        protected enum BooleanOperation
        {
            et_AND, ou_OR, non_NO, nonEt_NAND, nonOu_NOR, ouExclusif_XOR, nonOuExclusif_XNOR
        }


        ///<summary>
        ///Permet d'extraire dans une liste les booleen d'une liste d'objets actionnable (levier / pedestal).
        ///</summary>
        ///<param name= "objectActionnableList"> Une liste d'objet actionnable (levier, pedestal, etc) </param>
        ///<returns>
        ///Une liste de booleen extraite de la liste d'objet actioonnable (levier / pedestal)
        ///</returns>
        protected List<bool> conversionListeObjetActionnableAListeBooleen(List<ObjectActionnable> objectActionnableList)
        {
            List<bool> listeDeBool = new List<bool>();
            foreach (ObjectActionnable objectActionnable in objectActionnableList)
            {
                listeDeBool.Add(objectActionnable.Get_isActivated());
            }
            return listeDeBool;
        }


        ///<summary>
        ///Cette methode abstract force la classe enfant a l'utiliser. Elle est appelé par un objet actionnale (ex, levier)
        ///quand la veleur booleen de celui-ci est modifie
        ///</summary>
        public abstract void DetectionChangementObjetActionnable();



        ///<summary>
        ///Change la condition d'animation d'un Gameobject (ex, ouvre une porte en modifiant son paramettre dans l'animator).
        ///</summary>
        ///<param name= "condition"> valeur booleen qui dicte si l'animation est a true ou false </param>
        public void objectAnimatorSetParameterBool(bool condition)
        {
            //Debug.Log("Une porte a ete actionee a : "  +condition);
            objectToActivate.GetComponent<Animator>().SetBool(objectToActivateParameterName, condition);
        }


        /// <summary>
        /// Change la condition d'animation d'un Gameobject (ex, ouvre une porte en modifiant son paramettre dans l'animator).
        /// </summary>
        /// <param name="objetActionnable"> Un objet actionnable</param>
        public void objectAnimatorSetParameterBool(ObjectActionnable objetActionnable)
        {
            objectAnimatorSetParameterBool(objetActionnable.Get_isActivated());
        }
        /// <summary>
        /// La fonction qui s'occupe de resoudre le operation booleen
        /// </summary>
        /// <param name="operation">le type d'opration</param>
        /// <param name="x">un booleen</param>
        /// <param name="y">un deuxieme booleen</param>
        /// <returns></returns>
        protected bool ComparateurBooleen(BooleanOperation operation, bool x, bool y)
        {
            switch (operation)
            {
                case BooleanOperation.et_AND:
                    return x && y;
                case BooleanOperation.ou_OR:
                    return x || y;
                case BooleanOperation.non_NO:
                    return !x;
                case BooleanOperation.nonEt_NAND:
                    return !(x && y);
                case BooleanOperation.nonOu_NOR:
                    return !(x || y);
                case BooleanOperation.ouExclusif_XOR:
                    return !(x && y || !x && !y);
                case BooleanOperation.nonOuExclusif_XNOR:
                    return x && y || !x && !y;
                default:
                    return false;
            }
        }

        /// <summary>
        /// La fonction qui s'occupe de resoudre le operation booleen en passant des ObjectActionnable en parametre
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="objectActionnableX">un objet actionnable</param>
        /// <param name="objectActionnableY">un objet actionnable</param>
        /// <returns></returns>
        protected bool ComparateurBooleen(BooleanOperation operation, ObjectActionnable objectActionnableX, ObjectActionnable objectActionnableY)
        {
            return ComparateurBooleen(operation, objectActionnableX.Get_isActivated(), objectActionnableY.Get_isActivated());
        }


    }












}
