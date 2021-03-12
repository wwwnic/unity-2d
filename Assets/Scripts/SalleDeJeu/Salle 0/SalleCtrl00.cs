namespace SalleDeJeu
{
    /// <summary>
    /// Le controle d'une sale de jeu
    /// </summary>
    public class SalleCtrl00 : LogiqueDesSallesDeJeu
    {

        /// <summary>
        /// Vérifie si la salle est terminée.
        /// </summary>
        /// <returns>Si la salle est terminée</returns>
        public override bool CalculeBooleen()
        {
            return ComparateurBooleen(OperationBooleen.AND, objectActionnableList[0], objectActionnableList[1]);
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