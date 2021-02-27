using UnityEngine;


namespace SalleDeJeu
{
    public class SalleCtrl00 : LogiqueDesSalles
    {
        bool lever;

        private void Start()
        {
            bool lever = false;

        }

        public override void DetectionChangementObjetActionnable()
        {
            objectAnimatorSetParameterBool(objectActionnableList[0]);
        }
    }
}