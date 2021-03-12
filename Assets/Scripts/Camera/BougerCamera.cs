using UnityEngine;


/// <summary>
/// Gére le déplacement de la caméra selon la position du joueur.
/// </summary>
public class BougerCamera : MonoBehaviour
{
    private float _amortiCamera = 1.5f;
    private Transform _cible;
    private Vector2 _décalage = new Vector2(2f, 1f);
    private bool _regardeGauche;
    private int _dernierX;
    private float _vitesseDynamique;

   private void Start()
    {
        _décalage = new Vector2(Mathf.Abs(_décalage.x), _décalage.y);
        TrouverJoueur();
    }

    /// <summary>
    /// Trouve la position du joueur.
    /// </summary>
    private void TrouverJoueur()
    {
        _dernierX = Mathf.RoundToInt(_cible.position.x);
        transform.position = new Vector3(_cible.position.x + _décalage.x, _cible.position.y + _décalage.y, transform.position.z);
    }


   private void FixedUpdate()
    {
        if (_cible)
        {
            int positionX = Mathf.RoundToInt(_cible.position.x);
            _regardeGauche = positionX < _dernierX;
            _dernierX = Mathf.RoundToInt(_cible.position.x);

            Vector3 cible;
            if (_regardeGauche)
            {
                cible = new Vector3(_cible.position.x - _décalage.x, _cible.position.y + _décalage.y + _vitesseDynamique, transform.position.z);
            }
            else
            {
                cible = new Vector3(_cible.position.x + _décalage.x, _cible.position.y + _décalage.y + _vitesseDynamique, transform.position.z);
            }
            Vector3 positionActuel = Vector3.Lerp(transform.position, cible, _amortiCamera * Time.deltaTime);
            transform.position = positionActuel;
        }
    }
}
