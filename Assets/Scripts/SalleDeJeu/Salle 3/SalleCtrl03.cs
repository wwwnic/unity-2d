namespace SalleDeJeu
{
    /// <summary>
    /// Le controle d'une sale de jeu
    /// </summary>
    public class SalleCtrl03 : LogiqueDesSallesDeJeu
    {

        private bool _resultatColonneNO1_1;
        private bool _resultatColonneAND1_2;
        private bool _resultatColonneNOR1_3;
        private bool _resultatColonneOR1_4;

        private bool _resultatColonneNOR2_1;
        private bool _resultatColonneXOR2_2;
        private bool _resultatColonneNAND2_3;

        private bool _resultatColonneAND3_1;
        private bool _resultatColonneAND3_2;
        private bool _resultatFinalColonneAND4_1;


        /// <summary>
        /// Vérifie si la salle est terminée.
        /// </summary>
        /// <returns>Si la salle est terminée</returns>
        public override bool CalculeBooleen()
        {
            _resultatColonneNO1_1 = !(objectActionnableList[1].GetEstActive());
            _resultatColonneAND1_2 = ComparateurBooleen(OperationBooleen.AND, objectActionnableList[2], objectActionnableList[3]);
            _resultatColonneNOR1_3 = ComparateurBooleen(OperationBooleen.NOR, objectActionnableList[4], objectActionnableList[5]);
            _resultatColonneOR1_4 = ComparateurBooleen(OperationBooleen.OR, objectActionnableList[6], objectActionnableList[7]);

            _resultatColonneNOR2_1 = ComparateurBooleen(OperationBooleen.XOR, objectActionnableList[0].GetEstActive(), _resultatColonneNO1_1);
            _resultatColonneXOR2_2 = ComparateurBooleen(OperationBooleen.XNOR, _resultatColonneAND1_2, _resultatColonneNOR1_3);
            _resultatColonneNAND2_3 = ComparateurBooleen(OperationBooleen.NAND, _resultatColonneNOR1_3, _resultatColonneOR1_4);

            _resultatColonneAND3_1 = ComparateurBooleen(OperationBooleen.NOR, _resultatColonneNOR2_1, _resultatColonneXOR2_2);
            _resultatColonneAND3_2 = ComparateurBooleen(OperationBooleen.AND, _resultatColonneNOR1_3, _resultatColonneNAND2_3);

            _resultatFinalColonneAND4_1 = ComparateurBooleen(OperationBooleen.AND, _resultatColonneAND3_1, _resultatColonneAND3_2);

            return _resultatFinalColonneAND4_1;
        }

        /// <summary>
        /// Détecte un changement dans la salle.
        /// </summary>
        public override void DetectionChangementObjetActionnable()
        {
            changerParametreDansAnimator(CalculeBooleen());
        }
    }
}