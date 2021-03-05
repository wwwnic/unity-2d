using UnityEngine;


namespace SalleDeJeu
{
    public class SalleCtrl00 : LogiqueDesSallesDeJeu
    {
        public override bool CalculeBooleen()
        {
            return ComparateurBooleen(BooleanOperation.et_AND, objectActionnableList[0], objectActionnableList[1]);
        }

        public override void DetectionChangementObjetActionnable()
        {
            objectAnimatorSetParameterBool(CalculeBooleen());
        }
    }
}