using UnityEngine;


namespace SalleDeJeu
{
    public class SalleCtrl03 : LogiqueDesSalles
    {


        bool _resultatA_B;
        bool _resultatC_D;
        bool _resultatE;

        bool _cableCD_E;
        bool _resultatFinal;

        private void Awake()
        {
            
        }


        public override void DetectionChangementObjetActionnable()
        {
            _resultatA_B = CompratateurBoolean(booleanOperation.ouExclusif_XOR, objectActionnableList[0], objectActionnableList[1]);
            _resultatC_D = CompratateurBoolean(booleanOperation.ou_OR, objectActionnableList[2], objectActionnableList[3]);
            _resultatE = !objectActionnableList[4].Get_isActivated();
            _cableCD_E = transitionCable(_resultatC_D, _resultatE);
            _resultatFinal = !CompratateurBoolean(booleanOperation.nonEt_NAND, _resultatA_B, _cableCD_E);
            objectAnimatorSetParameterBool(_resultatFinal);
        }
    }
}