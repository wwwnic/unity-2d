using UnityEngine;
namespace SalleDeJeu
{

    public class SalleCtrl01 : LogiqueDesSalles
    {

        bool conditionPourTerminerLaSalle;
        private void Start()
        {
            conditionPourTerminerLaSalle = false;

        }

        public override void DetectionChangementObjetActionnable()
        {
            conditionPourTerminerLaSalle = CompratateurBoolean(booleanOperation.et_AND, objectActionnableList[0], objectActionnableList[1]); 
            objectAnimatorSetParameterBool(conditionPourTerminerLaSalle);
        }

    }
}