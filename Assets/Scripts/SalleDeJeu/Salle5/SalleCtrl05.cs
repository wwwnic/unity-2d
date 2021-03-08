namespace SalleDeJeu
{
    /// <summary>
    /// Le controle d'une sale de jeu
    /// </summary>
    public class SalleCtrl05 : LogiqueDesSallesDeJeu
    {
        private bool _resultatColonneNO1_1;
        private bool _resultatColonneNAND1_2;
        private bool _resultatColonneNOR1_3;

        private bool _resultatColonneNAND2_1;
        private bool _resultatColonneXOR2_2;
        private bool _resultatFinalColonneAND3_1;



        /// <summary>
        /// Vérifie si la salle est terminée.
        /// </summary>
        /// <returns>Si la salle est terminée</returns>
        public override bool CalculeBooleen()
        {
            _resultatColonneNO1_1 = !(objectActionnableList[0].GetIsActivated());
            _resultatColonneNAND1_2 = ComparateurBooleen(BooleanOperation.nonEt_NAND, objectActionnableList[1], objectActionnableList[2]);
            _resultatColonneNOR1_3 = ComparateurBooleen(BooleanOperation.nonOu_NOR, objectActionnableList[3], objectActionnableList[4]);

            _resultatColonneNAND2_1 = ComparateurBooleen(BooleanOperation.nonEt_NAND, _resultatColonneNO1_1, _resultatColonneNAND1_2);
            _resultatColonneXOR2_2 = ComparateurBooleen(BooleanOperation.ouExclusif_XOR, _resultatColonneNAND1_2, _resultatColonneNOR1_3);

            _resultatFinalColonneAND3_1 = ComparateurBooleen(BooleanOperation.et_AND, _resultatColonneNAND2_1, _resultatColonneXOR2_2);

            return _resultatFinalColonneAND3_1;
        }


        /// <summary>
        /// Détecte un changement dans une salle.
        /// </summary>
        public override void DetectionChangementObjetActionnable()
        {
            objectAnimatorSetParameterBool(CalculeBooleen());

        }
    }
}