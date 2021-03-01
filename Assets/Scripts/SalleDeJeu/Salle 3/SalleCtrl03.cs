using UnityEngine;


namespace SalleDeJeu
{
    public class SalleCtrl03 : LogiqueDesSalles
    {

        bool _resultatColooneNO1_1;
        bool _resultatColonneAND1_2;
        bool _resultatColonneNOR1_3;
        bool _resultatColonneOR1_4;


        bool _resultatColonneNOR2_1;
        bool _resultatColonneXOR2_2;
        bool _resultatColonneNAND2_3;


        bool _resultatColonneAND3_1;
        bool _resultatColonneAND3_2;


        bool _resultatFinalColonneAND4_1;





        public override void DetectionChangementObjetActionnable()
        {

            _resultatColooneNO1_1 = !objectActionnableList[1].Get_isActivated();
            _resultatColonneAND1_2 = CompratateurBoolean(booleanOperation.et_AND, objectActionnableList[2], objectActionnableList[3]);
            _resultatColonneNOR1_3 = CompratateurBoolean(booleanOperation.nonOu_NOR, objectActionnableList[4], objectActionnableList[5]);
            _resultatColonneOR1_4 = CompratateurBoolean(booleanOperation.ou_OR, objectActionnableList[6], objectActionnableList[7]);


            _resultatColonneNOR2_1 = CompratateurBoolean(booleanOperation.nonOu_NOR, objectActionnableList[0], _resultatColooneNO1_1);
            _resultatColonneXOR2_2 = CompratateurBoolean(booleanOperation.ouExclusif_XOR, _resultatColonneAND1_2, _resultatColonneNOR1_3);
            _resultatColonneNAND2_3 = CompratateurBoolean(booleanOperation.nonEt_NAND, _resultatColonneNOR1_3, _resultatColonneOR1_4);


            _resultatColonneAND3_1 = CompratateurBoolean(booleanOperation.et_AND, _resultatColonneNOR2_1, _resultatColonneXOR2_2);
            _resultatColonneAND3_2 = CompratateurBoolean(booleanOperation.et_AND, _resultatColonneNOR1_3, _resultatColonneNAND2_3);


            _resultatFinalColonneAND4_1 = CompratateurBoolean(booleanOperation.et_AND, _resultatColonneAND3_1, _resultatColonneAND3_2);


            objectAnimatorSetParameterBool(_resultatFinalColonneAND4_1);
        }
    }
}