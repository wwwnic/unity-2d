namespace SalleDeJeu
{
    interface IlogiqueDesSales
    {

        /// <summary>
        /// Détecte un changement dans une salle.
        /// </summary>
        void DetectionChangementObjetActionnable();

        /// <summary>
        /// Vérifie si la salle est terminée.
        /// </summary>
        /// <returns>Si la salle est terminée</returns>
        bool CalculeBooleen();
    }
}