namespace SalleDeJeu
{
    /// <summary>
    /// Le controle d'une sale de jeu
    /// </summary>
    public class SalleCtrl04 : LogiqueDesSallesDeJeu
    {
        private bool _resultatColonneOR1_1;
        private bool _resultatColonneNAND1_2;
        private bool _resultatColonneNO1_3;

        private bool _resultatColonneXNOR2_1;
        private bool _resultatColonneXNOR2_2;

        private bool _resultatFinalColonneAND3_1;

        /// <summary>
        /// Vérifie si la salle est terminée.
        /// </summary>
        /// <returns>Si la salle est terminée</returns>
        public override bool CalculeBooleen()
        {
            _resultatColonneOR1_1 = ComparateurBooleen(OperationBooleen.OR, objectActionnableList[0], objectActionnableList[1]);
            _resultatColonneNAND1_2 = ComparateurBooleen(OperationBooleen.NAND, objectActionnableList[2], objectActionnableList[3]);
            _resultatColonneNO1_3 = !objectActionnableList[4].GetEstActive();

            _resultatColonneXNOR2_1 = ComparateurBooleen(OperationBooleen.XNOR, _resultatColonneOR1_1, _resultatColonneNAND1_2);
            _resultatColonneXNOR2_2 = ComparateurBooleen(OperationBooleen.XNOR, _resultatColonneNO1_3, objectActionnableList[5].GetEstActive());

            _resultatFinalColonneAND3_1 = ComparateurBooleen(OperationBooleen.AND, _resultatColonneXNOR2_1, _resultatColonneXNOR2_2);
            return _resultatFinalColonneAND3_1;
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
