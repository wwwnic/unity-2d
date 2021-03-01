using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SalleDeJeu
{
    public abstract class LogiqueDesSalles : MonoBehaviour, IlogiqueDesSales
    {
        [Tooltip("La liste de levier")]
        [SerializeField] protected List<ObjectActionnable> objectActionnableList;
        [Tooltip("Nom du parametre dans l'Animator")]
        [SerializeField] protected string objectToActivateParameterName = "activated";
        [Tooltip("La porte à ouvrir")]
        [SerializeField] protected GameObject objectToActivate;



        protected List<bool> conversionListeObjetActionnableAListeBooleen(List<ObjectActionnable> objectActionnableList)
        {
            List<bool> listeDeBool = new List<bool>();
            foreach (ObjectActionnable objectActionnable in objectActionnableList)
            {
                listeDeBool.Add(objectActionnable.Get_isActivated());
            }
            return listeDeBool;
        }

        public abstract void DetectionChangementObjetActionnable();


        public void objectAnimatorSetParameterBool(bool condition)
        {
            //Debug.Log("Une porte a ete actionee a : "  +condition);
            objectToActivate.GetComponent<Animator>().SetBool(objectToActivateParameterName, condition);
        }


        public void objectAnimatorSetParameterBool(ObjectActionnable levier)
        {
            objectAnimatorSetParameterBool(levier.Get_isActivated());
        }


        protected enum BooleanOperation
        {
             et_AND, ou_OR, non_NO, nonEt_NAND, nonOu_NOR, ouExclusif_XOR, nonOuExclusif_XNOR
        }



        protected bool transitionCable(bool reponseX, bool reponseY)
        {
            return CompratateurBoolean(BooleanOperation.ou_OR, reponseX, reponseY);
        }


        protected bool transitionCable(List<bool> listeReponse)
        {  

            return CompratateurBoolean(BooleanOperation.ou_OR, listeReponse);
        }





        protected bool CompratateurBoolean(BooleanOperation operation, bool x, bool y)
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


        protected bool CompratateurBoolean(BooleanOperation operation, List<bool> listBool)
        {
            switch (operation)
            {
                case BooleanOperation.et_AND:
                    return listBool.TrueForAll(x => x);
                case BooleanOperation.ou_OR:
                    return listBool.Contains(true);
                case BooleanOperation.nonEt_NAND:
                    return !listBool.TrueForAll(x => x);
                case BooleanOperation.nonOu_NOR:
                    return !listBool.Contains(true);
                case BooleanOperation.ouExclusif_XOR:
                    return listBool.Contains(true) || listBool.TrueForAll(x => x);
                case BooleanOperation.nonOuExclusif_XNOR:
                    return !(listBool.Contains(true) || listBool.TrueForAll(x => x));
                default:
                    return false;
            }
        }

        protected bool CompratateurBoolean(BooleanOperation operation, ObjectActionnable objectActionnableX, ObjectActionnable objectActionnableY)
        {
            return CompratateurBoolean(operation, objectActionnableX.Get_isActivated(), objectActionnableY.Get_isActivated());
        }

        protected bool CompratateurBoolean(BooleanOperation operation, List<ObjectActionnable> listObjectActionnable)
        {
            return CompratateurBoolean(operation, conversionListeObjetActionnableAListeBooleen(listObjectActionnable));

        }

    }












}
