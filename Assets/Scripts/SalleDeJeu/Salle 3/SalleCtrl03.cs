﻿using UnityEngine;


namespace SalleDeJeu
{
    public class SalleCtrl03 : LogiqueDesSallesDeJeu
    {

        bool _resultatColonneNO1_1;
        bool _resultatColonneAND1_2;
        bool _resultatColonneNOR1_3;
        bool _resultatColonneOR1_4;

        bool _resultatColonneNOR2_1;
        bool _resultatColonneXOR2_2;
        bool _resultatColonneNAND2_3;

        bool _resultatColonneAND3_1;
        bool _resultatColonneAND3_2;
        bool _resultatFinalColonneAND4_1;

        public override bool CalculeBooleen()
        {
            _resultatColonneNO1_1 = !(objectActionnableList[1].GetIsActivated());
            _resultatColonneAND1_2 = ComparateurBooleen(BooleanOperation.et_AND, objectActionnableList[2], objectActionnableList[3]);
            _resultatColonneNOR1_3 = ComparateurBooleen(BooleanOperation.nonOu_NOR, objectActionnableList[4], objectActionnableList[5]);
            _resultatColonneOR1_4 = ComparateurBooleen(BooleanOperation.ou_OR, objectActionnableList[6], objectActionnableList[7]);

            _resultatColonneNOR2_1 = ComparateurBooleen(BooleanOperation.nonOu_NOR, objectActionnableList[0].GetIsActivated(), _resultatColonneNO1_1);
            _resultatColonneXOR2_2 = ComparateurBooleen(BooleanOperation.ouExclusif_XOR, _resultatColonneAND1_2, _resultatColonneNOR1_3);
            _resultatColonneNAND2_3 = ComparateurBooleen(BooleanOperation.nonEt_NAND, _resultatColonneNOR1_3, _resultatColonneOR1_4);

            _resultatColonneAND3_1 = ComparateurBooleen(BooleanOperation.et_AND, _resultatColonneNOR2_1, _resultatColonneXOR2_2);
            _resultatColonneAND3_2 = ComparateurBooleen(BooleanOperation.et_AND, _resultatColonneNOR1_3, _resultatColonneNAND2_3);

            _resultatFinalColonneAND4_1 = ComparateurBooleen(BooleanOperation.et_AND, _resultatColonneAND3_1, _resultatColonneAND3_2);

            return _resultatFinalColonneAND4_1;
        }

        public override void DetectionChangementObjetActionnable()
        {
            objectAnimatorSetParameterBool(CalculeBooleen());
        }
    }
}