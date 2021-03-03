using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SalleDeJeu
{
    public class SalleCtrl04 : LogiqueDesSalles
    {
        bool _resultatColonneOR1_1;
        bool _resultatColonneNAND1_2;
        bool _resultatColonneNO1_3;

        bool _resultatColonneXNOR2_1;
        bool _resultatColonneXNOR2_2;

        bool _resultatFinalColonneAND3_1;

        public override void DetectionChangementObjetActionnable()
        {
            _resultatColonneOR1_1 = ComparateurBooleen(BooleanOperation.ou_OR, objectActionnableList[0], objectActionnableList[1]);
            _resultatColonneNAND1_2 = ComparateurBooleen(BooleanOperation.nonEt_NAND, objectActionnableList[2], objectActionnableList[3]);
            _resultatColonneNO1_3 = !objectActionnableList[4].Get_isActivated();

            _resultatColonneXNOR2_1 = ComparateurBooleen(BooleanOperation.nonOuExclusif_XNOR, _resultatColonneOR1_1, _resultatColonneNAND1_2);
            _resultatColonneXNOR2_2 = ComparateurBooleen(BooleanOperation.nonOuExclusif_XNOR, _resultatColonneNO1_3, objectActionnableList[5].Get_isActivated());


            _resultatFinalColonneAND3_1 = ComparateurBooleen(BooleanOperation.et_AND, _resultatColonneXNOR2_1, _resultatColonneXNOR2_2);
            objectAnimatorSetParameterBool(_resultatFinalColonneAND3_1);

        }
    }
}