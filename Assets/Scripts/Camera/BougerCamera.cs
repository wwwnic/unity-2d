using UnityEngine;


/// <summary>
/// Gére le déplacement de la caméra selon la position du joueur.
/// </summary>
public class BougerCamera : MonoBehaviour
{
    public float amortiCamera = 1.5f;
    public Transform _cible;
    public Vector2 décalage = new Vector2(2f, 1f);

    private bool regardeGauche;
    private int dernierX;
    private float vitesseDynamique;

    void Start()
    {
        décalage = new Vector2(Mathf.Abs(décalage.x), décalage.y);
        TrouverJoueur();
    }

    /// <summary>
    /// Trouve la position du joueur.
    /// </summary>
    public void TrouverJoueur()
    {
        dernierX = Mathf.RoundToInt(_cible.position.x);
        transform.position = new Vector3(_cible.position.x + décalage.x, _cible.position.y + décalage.y, transform.position.z);
    }


    void FixedUpdate()
    {
        if (_cible)
        {
            int currentX = Mathf.RoundToInt(_cible.position.x);
            if (currentX > dernierX) regardeGauche = false; else if (currentX < dernierX) regardeGauche = true;
            dernierX = Mathf.RoundToInt(_cible.position.x);

            Vector3 target;
            if (regardeGauche)
            {
                target = new Vector3(_cible.position.x - décalage.x, _cible.position.y + décalage.y + vitesseDynamique, transform.position.z);
            }
            else
            {
                target = new Vector3(_cible.position.x + décalage.x, _cible.position.y + décalage.y + vitesseDynamique, transform.position.z);
            }
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, amortiCamera * Time.deltaTime);
            transform.position = currentPosition;
        }
    }
}
