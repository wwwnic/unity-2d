namespace SalleDeJeu
{
    /// <summary>
    /// Le controle d'une sale de jeu
    /// </summary>
    public class SalleCtrl02 : LogiqueDesSallesDeJeu
    {
        private bool _resultatColonneXNOR1_1;
        private bool _resultatColonneXOR1_2;
        private bool _resultatFinalColonneNAND2_1;

        /// <summary>
        /// Vérifie si la salle est terminée.
        /// </summary>
        /// <returns>Si la salle est terminée</returns>
        public override bool CalculeBooleen()
        {
            _resultatColonneXNOR1_1 = ComparateurBooleen(BooleanOperation.nonOuExclusif_XNOR, objectActionnableList[0], objectActionnableList[1]);
            _resultatColonneXOR1_2 = ComparateurBooleen(BooleanOperation.ouExclusif_XOR, objectActionnableList[2], objectActionnableList[3]);

            _resultatFinalColonneNAND2_1 = ComparateurBooleen(BooleanOperation.et_AND, _resultatColonneXNOR1_1, _resultatColonneXOR1_2);

            return _resultatFinalColonneNAND2_1;
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