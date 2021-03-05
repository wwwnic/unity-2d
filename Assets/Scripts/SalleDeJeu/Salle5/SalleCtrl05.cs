using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SalleDeJeu
{
    public class SalleCtrl05 : LogiqueDesSallesDeJeu
    {
        bool _resultatColonneNO1_1;
        bool _resultatColonneNAND1_2;
        bool _resultatColonneNOR1_3;

        bool _resultatColonneNAND2_1;
        bool _resultatColonneXOR2_2;

        bool _resultatFinalColonneAND3_1;

        public override bool CalculeBooleen()
        {
            _resultatColonneNO1_1 = !(objectActionnableList[0].Get_isActivated());
            _resultatColonneNAND1_2 = ComparateurBooleen(BooleanOperation.nonEt_NAND, objectActionnableList[1], objectActionnableList[2]);
            _resultatColonneNOR1_3 = ComparateurBooleen(BooleanOperation.nonOu_NOR, objectActionnableList[3], objectActionnableList[4]);

            _resultatColonneNAND2_1 = ComparateurBooleen(BooleanOperation.nonEt_NAND, _resultatColonneNO1_1, _resultatColonneNAND1_2);
            _resultatColonneXOR2_2 = ComparateurBooleen(BooleanOperation.ouExclusif_XOR, _resultatColonneNAND1_2, _resultatColonneNOR1_3);


            _resultatFinalColonneAND3_1 = ComparateurBooleen(BooleanOperation.et_AND, _resultatColonneNAND2_1, _resultatColonneXOR2_2);

            return _resultatFinalColonneAND3_1;
        }

        public override void DetectionChangementObjetActionnable()
        {
            objectAnimatorSetParameterBool(CalculeBooleen());

        }
    }
}