using UnityEngine;


namespace SalleDeJeu
{
    public class SalleCtrl00 : LogiqueDesSalles
    {

        public override void DetectionChangementObjetActionnable()
        {
            objectAnimatorSetParameterBool(ComparateurBooleen(BooleanOperation.et_AND, objectActionnableList[0], objectActionnableList[1]));
        }
    }
}