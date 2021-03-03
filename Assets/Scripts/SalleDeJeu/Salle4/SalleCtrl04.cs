using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SalleDeJeu
{
    public class SalleCtrl04 : LogiqueDesSalles
    {
        bool _resultatColonneOR1_2;
        bool _resultatColonneNAND3_4;
        bool _resultatColonneNOT5;
        bool _resultatColonneXNOR1_2;
        bool _resultatColonneXNOR3_4;
        bool _resultatFinalColonneAND;

        public override void DetectionChangementObjetActionnable()
        {
            _resultatColonneOR1_2 = ComparateurBooleen(BooleanOperation.ou_OR, objectActionnableList[0], objectActionnableList[1]);
            _resultatColonneNAND3_4 = ComparateurBooleen(BooleanOperation.nonEt_NAND, objectActionnableList[2], objectActionnableList[3]);
            _resultatColonneNOT5 = ComparateurBooleen(BooleanOperation.non_NO, objectActionnableList[4], objectActionnableList[4]);
            _resultatColonneXNOR1_2 = ComparateurBooleen(BooleanOperation.nonOuExclusif_XNOR, _resultatColonneOR1_2, _resultatColonneNAND3_4);
            _resultatColonneXNOR3_4 = ComparateurBooleen(BooleanOperation.nonOuExclusif_XNOR, _resultatColonneNOT5, objectActionnableList[5]);

            _resultatFinalColonneAND = ComparateurBooleen(BooleanOperation.et_AND, _resultatColonneXNOR1_2, _resultatColonneXNOR3_4);

            objectAnimatorSetParameterBool(_resultatFinalColonneAND);

        }
    }
}