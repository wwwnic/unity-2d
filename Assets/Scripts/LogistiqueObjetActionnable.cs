using System.Collections.Generic;
using UnityEngine;


namespace ObjetActionnable
{
    public class LogistiqueObjetActionnable : MonoBehaviour
    {
        [Tooltip("Ne nombre de levier, 0 et 1 seront comparés ainsi que 2,3 et 4,5 etc...")]
        [SerializeField] ObjectActionnable[] objectActionnableList;
        [Tooltip("Ce nombre est le nombre de levier divisé par deux. (size) indique le nombres de comparateur, vous devrez ensuite donner les opérations.")]
        [SerializeField] booleanOperation[] operationMathematiqueÉtage1;
        [Tooltip("Cet étage calculera les résultat de l'étage 1 et les lanceras vers le prochains étage s'il y a lieu (0 et 1 seront comparés ainsi que 2,3 et 4,5, etc...) (size) indique le nombres de comparateur, vous devrez ensuite donner les opérations.")]
        [SerializeField] booleanOperation[] operationMathematiqueÉtage2;
        [Tooltip("voir étage 2 et faire suivre la logique")]
        [SerializeField] booleanOperation[] operationMathematiqueÉtage3;
        [Tooltip("voir étage 2 et faire suivre la logique")]
        [SerializeField] booleanOperation[] operationMathematiqueÉtage4;



        public enum booleanOperation
        {
            none, et_AND, ou_OR, non_NO, nonEt_NAND, nonOu_NOR, ouExclusif_XOR, nonOuExclusif_XNOR, solution, erreur
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }




        //a besoin d'un d'être divisé en deux avec un refactoring - Nicolas
        //
        public void ChangementObjetActionnable() 
        {
            List<bool> resultatÉtage1 = null;
            List<bool> resultatÉtage2 = null;
            List<bool> resultatÉtage3 = null;
            List<bool> resultatÉtage4 = null;


            List<bool> listeDeResultat = new List<bool>();
            booleanOperation typeOperation;
            int operationIterator = 0;
            bool resultatOperation;


            bool estPasTeminee = true;

            //spaghetti bol, just pour que ça marche rapidment
            resultatÉtage1 = ComparerListBooleanAvecComparateur(ListeObjetActionnableAListeBooleen());
        }

        private bool aTermineeLaSale(List<bool> listeBool)
        {
            //implmenter une action x (EX: ouvrir porte)

            return !(listeBool.Count <= 1);

        }



        private List<bool> ListeObjetActionnableAListeBooleen()
        {
            List<bool> listeDeBool = new List<bool>();

            foreach (ObjectActionnable objectActionnable in objectActionnableList)
            {
                listeDeBool.Add(objectActionnable.Get_isActivated());
            }
            return listeDeBool;
        }


        private List<bool> ComparerListBooleanAvecComparateur(List<bool> listeDeBool)
        {
            bool groupeDeDeuxObjetActionnable = true;
            List<bool> listeDeResultat = new List<bool>();
            booleanOperation typeOperation;
            int operationIterator = 0;
            bool resultatOperation;

            for (int i = 0; i < objectActionnableList.Length; i++)
            {
                if (groupeDeDeuxObjetActionnable)
                {
                    bool premierObjetActionnable = listeDeBool[i];
                    bool secondObjetActionnable = listeDeBool[i + 1];

                    typeOperation = (booleanOperation)operationMathematiqueÉtage1.GetValue(operationIterator);
                    operationIterator++;
                    resultatOperation = CompratateurBoolean(premierObjetActionnable, secondObjetActionnable, typeOperation);
                    listeDeResultat.Add(resultatOperation);
                    Debug.Log("Operation logique entre " + i + " et " + (i + 1) + " (" + typeOperation + ")= " + resultatOperation);

                }
                groupeDeDeuxObjetActionnable = !groupeDeDeuxObjetActionnable;
            }
            return listeDeResultat;
        }




        //Logique sera probalement supprimé pour éviter les appels de fonctions
        private bool CompratateurBoolean(bool a, bool b, booleanOperation typeOperation)
        {
            switch (typeOperation)
            {
                case booleanOperation.et_AND:
                    return Logique.FonctionEt(a, b);
                case booleanOperation.ou_OR:
                    return Logique.FonctionOu(a, b);
                case booleanOperation.non_NO:
                    return Logique.FonctionNon(a);
                case booleanOperation.nonEt_NAND:
                    return Logique.FonctionNonEt(a, b);
                case booleanOperation.nonOu_NOR:
                    return Logique.FonctionNonOu(a, b);
                case booleanOperation.ouExclusif_XOR:
                    return Logique.FonctionOuExclusif(a, b);
                case booleanOperation.nonOuExclusif_XNOR:
                    return Logique.FonctionNonOuExclusif(a, b);
                default:
                    return false;
            }
        }
    }
}