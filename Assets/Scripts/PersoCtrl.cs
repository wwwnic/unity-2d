using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script de contrôle du personnage.
/// </summary>
public class PersoCtrl : MonoBehaviour
{
    // Vitesse de marche
    [SerializeField] float vitesse = 2f;

    // Vitesse du saut
    [SerializeField] float vitesseSautInitiale = 5f;

    // Amorti du saut
    [SerializeField] float amortiSaut = 0.1f;

    // Layers considérées comme le "sol"
    [SerializeField] LayerMask LayerSol;

    // Layer considérée comme le mur
    [SerializeField] LayerMask LayerMur;

    // Layer contenant les objets qui peuvent être pris par le personnage
    [SerializeField] LayerMask LayerGrabbable;

    // La collision pour détecter un object à prendre.
    [SerializeField] Transform grabHitbox;
    // La position de l'objet tenu.
    [SerializeField] Transform itemHolder;
    // La distance de grabHitbox.
    [SerializeField] float rayDist;

    private bool _isGrabbing = false;
    private GameObject _grabbedItem;


    Rigidbody2D rb;
    Animator anim;
    new CapsuleCollider2D collider;

    private bool _regarderDroite = true;
    private bool _isJumping = false;
    private float _vitesseSaut;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("deplacement", Mathf.Abs(rb.velocity.x));
        FonceDansMurDroite();
        FonceDansMurGauche();
    }

    /// <summary>
    /// Faire marcher le personnage vers la droite.
    /// </summary>
    public void Avancer()
    {
        if (!FonceDansMurDroite())
        {
            rb.velocity = new Vector2(vitesse, rb.velocity.y);
        }
        if (!_regarderDroite)
        {
            _regarderDroite = true;
            Retourner();
        }
    }

    /// <summary>
    /// Faire marcher le personnage vers la gauche.
    /// </summary>
    public void Reculer()
    {

        if (!FonceDansMurGauche())
        {
            rb.velocity = new Vector2(-vitesse, rb.velocity.y);
        }
        if (_regarderDroite)
        {
            _regarderDroite = false;
            Retourner();
        }
    }

    /// <summary>
    /// Retourner le personnage quand on appuie la direction inverse.
    /// </summary>
    public void Retourner()
    {
        Vector2 scale = new Vector2(-transform.localScale.x, transform.localScale.y);

        transform.localScale = scale;
    }

    /// <summary>
    /// Faire attaquer le personnage.
    /// </summary>
    public void Attaquer()
    {
        anim.SetTrigger("attaque");
    }

    /// <summary>
    /// Fonction pour prendre un objet.
    /// </summary>
    public void Prendre()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabHitbox.position, Vector2.right * transform.localScale,
           rayDist,LayerGrabbable);
        if (!_isGrabbing && grabCheck.collider != null && grabCheck.collider.tag == "box")
        {
            _grabbedItem = grabCheck.collider.gameObject;
            if (!_isGrabbing)
            {
                // Prendre l'item
                _grabbedItem.transform.parent = itemHolder;
                _grabbedItem.transform.position = itemHolder.position;
                _grabbedItem.GetComponent<Rigidbody2D>().isKinematic = true;
                _grabbedItem.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                _isGrabbing = true;
            }
        }
    }

    /// <summary>
    /// Vérifier que le personnage est en train de prendre un objet ou non.
    /// </summary>
    /// <returns>Vrai ou faux</returns>
    public bool JoueurIsGrabbing()
    {
        return _isGrabbing;
    }

    /// <summary>
    /// Laisser tomber l'objet tenu.
    /// </summary>
    public void Laisser()
    {
        if (_isGrabbing)
        {
            // Laisser l'item
            _grabbedItem.transform.parent = null;
            _grabbedItem.transform.position = grabHitbox.transform.position;
            _grabbedItem.GetComponent<Rigidbody2D>().isKinematic = false;
            _grabbedItem.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            _isGrabbing = false;
        }
    }

    /// <summary>
    /// Début du saut.
    /// </summary>
    public void SauterDebut()
    {
        if(!_isJumping && EstSurLeSol())
        {
            _isJumping = true;
            _vitesseSaut = vitesseSautInitiale;
        }
    }

    /// <summary>
    /// Gestion du saut.
    /// </summary>
    public void Sauter()
    {
        if(_isJumping)
        {
            anim.SetBool("jumping", true);
            rb.velocity += Vector2.up * _vitesseSaut;
            _vitesseSaut -= amortiSaut;
            if (_vitesseSaut < 0)
            {
                _vitesseSaut = 0;
                _isJumping = false;
            }
        }
        if (EstSurLeSol())
        {
            anim.SetBool("jumping", false);
        }
    }

    /// <summary>
    /// Vérification de l'atterissage.
    /// </summary>
    public void SauterFin()
    {
        _isJumping = false;
        if (EstSurLeSol())
        {
            anim.SetBool("jumping", false);
            anim.SetBool("estSurSol", true);

        }
    }

    /// <summary>
    /// Vérifier si le personnage est au sol.
    /// </summary>
    /// <returns>Vrai ou faux</returns>
    public bool EstSurLeSol()
    {
        Vector3 centerLeft = collider.bounds.center;
        centerLeft[0] = collider.bounds.min.x;
        Vector3 centerRight = collider.bounds.center;
        centerRight[0] = collider.bounds.max.x;
        float ajustement = 0.08f;
        RaycastHit2D raycastHitCenter = Physics2D.Raycast(collider.bounds.center,
            Vector2.down, collider.bounds.extents.y + ajustement, LayerSol);
        RaycastHit2D raycastHitLeft = Physics2D.Raycast(centerLeft,
            Vector2.down, collider.bounds.extents.y + ajustement, LayerSol);
        RaycastHit2D raycastHitRight = Physics2D.Raycast(centerRight,
            Vector2.down, collider.bounds.extents.y + ajustement, LayerSol);

        Color rayColor;
        if (raycastHitLeft.collider != null || raycastHitCenter.collider != null || raycastHitRight.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(collider.bounds.center,
    Vector2.down * (collider.bounds.extents.y + ajustement), rayColor);
        Debug.Log(raycastHit.collider);


        Color rayColor;
        if (raycastHitRight.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(collider.bounds.center,
            Vector2.right * (collider.bounds.extents.y + ajustement), rayColor);

        return raycastHitRight.collider != null;
    }

    /// <summary>
    /// Vérifier si on fonce dans le mur gauche.
    /// </summary>
    /// <returns>Vrai ou faux</returns>
    public bool FonceDansMurGauche()
    {
        float ajustement = 0.5f;

        RaycastHit2D raycastHitLeft = Physics2D.Raycast(collider.bounds.center,
    Vector2.left, collider.bounds.extents.x + ajustement, LayerSol);



        Color rayColor;
        if (raycastHitLeft.collider != null )
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(collider.bounds.center,
    Vector2.left * (collider.bounds.extents.x + ajustement), rayColor);
        Debug.Log(raycastHitLeft.collider);

        return raycastHitLeft.collider != null;
    }

    /// <summary>
    /// Vérifier si on fonce dans le mur droit.
    /// </summary>
    /// <returns>Vrai ou faux</returns>
    public bool FonceDansMurDroite()
    {
        float ajustement = 0.5f;

        RaycastHit2D raycastHitRight = Physics2D.Raycast(collider.bounds.center,
    Vector2.right, collider.bounds.extents.x + ajustement, LayerSol);



        Color rayColor;
        if (raycastHitRight.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(collider.bounds.center,
    Vector2.right * (collider.bounds.extents.x + ajustement), rayColor);
        Debug.Log(raycastHitRight.collider);

        return raycastHitRight.collider != null;
    }

}
