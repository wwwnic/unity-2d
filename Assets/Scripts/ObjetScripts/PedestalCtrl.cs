using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script de contrôle des pédestals.
/// </summary>
public class PedestalCtrl : MonoBehaviour
{
    // L'identificateur du pédestal.
    [SerializeField] int pedestalTag;
    // Les deux côtés du pédestal qui s'allument selon les boîtes placées dessus.
    [SerializeField] SpriteRenderer spriteGauche;
    [SerializeField] SpriteRenderer spriteDroit;
    // L'objet activé par le pédestal/levier.
    [SerializeField] GameObject activatedObject1;
    // Les pédestals additionnels requis pour activer celui-ci
    [SerializeField] List<GameObject> listePedestal;

    private BoxCollider2D boxDetector;

    private bool _hasRed = false;
    private bool _hasBlue= false;
    // Start is called before the first frame update
    void Start()
    {
        boxDetector = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Utilisé par le levier pour activer le pédestal selon les items dessus selon l'id de ce dernier.
    /// </summary>
    public void ActivatePedestal()
    {
        switch (pedestalTag)
        {
            case 1:
                if (_hasRed || _hasBlue)
                {
                    activatedObject1.GetComponent<Animator>().SetTrigger("activated");
                }
                break;
            case 2:
                PedestalCtrl pedestal1 = listePedestal[0].GetComponent<PedestalCtrl>();
                if (_hasBlue && _hasRed)
                {
                    if ((pedestal1.HasRed() || pedestal1.HasBlue()) &&
                        !(pedestal1.HasRed() && pedestal1.HasBlue()))
                    {
                        activatedObject1.GetComponent<Animator>().SetTrigger("activated");
                    }
                }
                break;
        }
    }

    /// <summary>
    /// Vérifie si il a un objet rouge.
    /// </summary>
    /// <returns>Vrai ou faux</returns>
    public bool HasRed()
    {
        return _hasRed;
    }

    /// <summary>
    /// Vérifie si il a un objet bleu.
    /// </summary>
    /// <returns>Vrai ou faux</returns>
    public bool HasBlue()
    {
        return _hasBlue;
    }

    /// <summary>
    /// Détecte les objets placés sur le pédestal.
    /// </summary>
    /// <param name="other">Les objets placés</param>
    private void OnTriggerStay2D(Collider2D other)
    {
        BoxCtrl boxCtrl = other.gameObject.GetComponent<BoxCtrl>();
        if (boxCtrl)
        {
            if (boxCtrl.getBoxType() == "red")
            {
                _hasRed = true;
                spriteGauche.color = Color.red;
            }
            else if (boxCtrl.getBoxType() == "blue")
            {
                _hasBlue = true;
                spriteDroit.color = Color.blue;
            }
        }
    }

    /// <summary>
    /// Détecte les objets retirés du pédestal.
    /// </summary>
    /// <param name="other">Les objets rétirés</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        BoxCtrl boxCtrl = other.gameObject.GetComponent<BoxCtrl>();
        if (boxCtrl)
        {
            if (boxCtrl.getBoxType() == "red")
            {
                _hasRed = false;
                spriteGauche.color = new Color(0.3537736f, 0.822655f, 1.0f);
            }
            else if (boxCtrl.getBoxType() == "blue")
            {
                _hasBlue = false;
                spriteDroit.color = new Color(0.3537736f, 0.822655f, 1.0f);
            }
        }
    }

}
