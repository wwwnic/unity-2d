using UnityEngine;


namespace SalleDeJeu
{
    public class SalleCtrl02 : LogiqueDesSalles
    {
        public override void DetectionChangementObjetActionnable()
        {
            objectAnimatorSetParameterBool(CompratateurBoolean(booleanOperation.ouExclusif_XOR, objectActionnableList[0], objectActionnableList[1]));
        }
    }
}