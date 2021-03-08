namespace SalleDeJeu
{
    public class SalleCtrl02 : LogiqueDesSallesDeJeu
    {
        bool _resultatColonneXNOR1_1;
        bool _resultatColonneXOR1_2;
        bool _resultatFinalColonneNAND2_1;

        public override bool CalculeBooleen()
        {
            _resultatColonneXNOR1_1 = ComparateurBooleen(BooleanOperation.nonOuExclusif_XNOR, objectActionnableList[0], objectActionnableList[1]);
            _resultatColonneXOR1_2 = ComparateurBooleen(BooleanOperation.ouExclusif_XOR, objectActionnableList[2], objectActionnableList[3]);

            _resultatFinalColonneNAND2_1 = ComparateurBooleen(BooleanOperation.et_AND, _resultatColonneXNOR1_1, _resultatColonneXOR1_2);

            return _resultatFinalColonneNAND2_1;
        }

        public override void DetectionChangementObjetActionnable()
        {
            objectAnimatorSetParameterBool(CalculeBooleen());
        }
    }
}