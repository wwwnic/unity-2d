using UnityEngine;
namespace SalleDeJeu
{

    public class SalleCtrl01 : LogiqueDesSalles
    {

        bool _resultatColonneNO1_3;


        bool _resultatColonneAND2_1;
        bool _resultatColonneNOR2_2;

        bool _resultatFinalColonneAND3_1;

        public override void DetectionChangementObjetActionnable()
        {
            
            _resultatColonneNO1_3 = !(objectActionnableList[2].Get_isActivated());


            _resultatColonneAND2_1 = ComparateurBooleen(BooleanOperation.et_AND, objectActionnableList[0], objectActionnableList[1]);
            _resultatColonneNOR2_2 = ComparateurBooleen(BooleanOperation.ouExclusif_XOR, objectActionnableList[1], _resultatColonneNO1_3);


            _resultatFinalColonneAND3_1 = ComparateurBooleen(BooleanOperation.et_AND, _resultatColonneAND2_1, _resultatColonneNOR2_2);




            objectAnimatorSetParameterBool(_resultatFinalColonneAND3_1);
        }

    }
}