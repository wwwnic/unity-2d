namespace SalleDeJeu
{

    public class SalleCtrl01 : LogiqueDesSallesDeJeu
    {

        bool _resultatColonneNO1_3;
        bool _resultatColonneAND2_1;
        bool _resultatColonneNOR2_2;
        bool _resultatFinalColonneAND3_1;

        public override bool CalculeBooleen()
        {
            _resultatColonneNO1_3 = !(objectActionnableList[2].GetIsActivated());

            _resultatColonneAND2_1 = ComparateurBooleen(BooleanOperation.et_AND, objectActionnableList[0], objectActionnableList[1]);
            _resultatColonneNOR2_2 = ComparateurBooleen(BooleanOperation.ouExclusif_XOR, objectActionnableList[1], _resultatColonneNO1_3);

            _resultatFinalColonneAND3_1 = ComparateurBooleen(BooleanOperation.et_AND, _resultatColonneAND2_1, _resultatColonneNOR2_2);
            return _resultatFinalColonneAND3_1;
        }

        public override void DetectionChangementObjetActionnable()
        {
            objectAnimatorSetParameterBool(CalculeBooleen());
        }




    }
}