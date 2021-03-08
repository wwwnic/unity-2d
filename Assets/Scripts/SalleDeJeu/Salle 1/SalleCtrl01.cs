namespace SalleDeJeu
{
    /// <summary>
    /// Le controle d'une sale de jeu
    /// </summary>
    public class SalleCtrl01 : LogiqueDesSallesDeJeu
    {
       private bool _resultatColonneNO1_3;
       private bool _resultatColonneAND2_1;
       private bool _resultatColonneNOR2_2;
       private bool _resultatFinalColonneAND3_1;

        /// <summary>
        /// Vérifie si la salle est terminée.
        /// </summary>
        /// <returns>Si la salle est terminée</returns>
        public override bool CalculeBooleen()
        {
            _resultatColonneNO1_3 = !(objectActionnableList[2].GetIsActivated());

            _resultatColonneAND2_1 = ComparateurBooleen(BooleanOperation.et_AND, objectActionnableList[0], objectActionnableList[1]);
            _resultatColonneNOR2_2 = ComparateurBooleen(BooleanOperation.ouExclusif_XOR, objectActionnableList[1], _resultatColonneNO1_3);

            _resultatFinalColonneAND3_1 = ComparateurBooleen(BooleanOperation.et_AND, _resultatColonneAND2_1, _resultatColonneNOR2_2);
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