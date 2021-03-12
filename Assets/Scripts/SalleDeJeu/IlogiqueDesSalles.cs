namespace SalleDeJeu
{
    interface IlogiqueDesSalles
    {

        /// <summary>
        /// Détecte un changement dans une salle (ex: activer un levier).
        /// </summary>
        void DetectionChangementObjetActionnable();

        /// <summary>
        /// Vérifie si la salle est terminée en effectuant les calcules de la salle.
        /// </summary>
        /// <returns>Si la salle est terminée</returns>
        bool CalculeBooleen();
    }
}