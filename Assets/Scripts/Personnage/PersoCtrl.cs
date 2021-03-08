using UnityEngine;

/// <summary>
/// Script de contrôle du personnage.
/// </summary>
public class PersoCtrl : MonoBehaviour
{
    // Vitesse de marche
    [SerializeField] float vitesse = 2f;

    // Vitesse du saut
    [SerializeField] float puissanceDuSaut = 5f;

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


    Rigidbody2D _rb;
    Animator _anim;
    CapsuleCollider2D _collider;

    private bool _regarderDroite = true;
    bool _estAuSol;
    bool _estEnSaut = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider2D>();
        _estAuSol = true;
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetFloat("deplacement", Mathf.Abs(_rb.velocity.x));
        _estAuSol = EstSurLeSol();
        _anim.SetBool("jumping", !(_estAuSol));
    }

    /// <summary>
    /// Gestion du saut.
    /// </summary>
    public void Sauter()
    {
        if (_estAuSol && !_estEnSaut)
        {
            _estEnSaut = true;
            _rb.velocity += Vector2.up * puissanceDuSaut;

        }
    }
    /// <summary>
    /// Empêche le joueur de devenir superman
    /// </summary>
    public void SauterFin()
    {
        if (_estAuSol)
        {
            _estEnSaut = false;
        }
    }


    /// <summary>
    /// Faire marcher le personnage vers la droite.
    /// </summary>
    public void Avancer()
    {
        if (!FonceDansMurDroite())
        {
            _rb.velocity = new Vector2(vitesse, _rb.velocity.y);
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
            _rb.velocity = new Vector2(-vitesse, _rb.velocity.y);
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
        _anim.SetTrigger("attaque");
    }


    /// <summary>
    /// Vérifier si le personnage est au sol.
    /// </summary>
    /// <returns>Vrai ou faux</returns>
    public bool EstSurLeSol()
    {
        Vector3 centerLeft = _collider.bounds.center;
        centerLeft[0] = _collider.bounds.min.x;
        Vector3 centerRight = _collider.bounds.center;
        centerRight[0] = _collider.bounds.max.x;
        float ajustement = 0.07f;

        RaycastHit2D raycastHitCenter = Physics2D.Raycast(_collider.bounds.center, Vector2.down, _collider.bounds.extents.y + ajustement, LayerSol);
        RaycastHit2D raycastHitLeft = Physics2D.Raycast(centerLeft, Vector2.down, _collider.bounds.extents.y + ajustement, LayerSol);
        RaycastHit2D raycastHitRight = Physics2D.Raycast(centerRight, Vector2.down, _collider.bounds.extents.y + ajustement, LayerSol);


        bool toucheLeSol = raycastHitLeft.collider != null || raycastHitCenter.collider != null || raycastHitRight.collider != null;
        Color rayColor = (toucheLeSol ? Color.green : Color.red);

        Debug.DrawRay(_collider.bounds.center,
            Vector2.down * (_collider.bounds.extents.y + ajustement), rayColor);
        Debug.DrawRay(centerLeft,
            Vector2.down * (_collider.bounds.extents.y + ajustement), rayColor);
        Debug.DrawRay(centerRight,
            Vector2.down * (_collider.bounds.extents.y + ajustement), rayColor);

        return toucheLeSol;
    }

    /// <summary>
    /// Vérifier si on fonce dans le mur gauche.
    /// </summary>
    /// <returns>Vrai ou faux</returns>
    public bool FonceDansMurGauche()
    {
        bool detecteUneColision;
        float ajustement = 0.1f;
        RaycastHit2D raycastHitLeft = Physics2D.Raycast(_collider.bounds.center,
             Vector2.left, _collider.bounds.extents.x + ajustement, LayerMur);

        detecteUneColision = raycastHitLeft.collider != null;
        Color rayColor = (detecteUneColision ? Color.red : Color.green);
        Debug.DrawRay(_collider.bounds.center,
            Vector2.left * (_collider.bounds.extents.x + ajustement), rayColor);
        return detecteUneColision;
    }

    /// <summary>
    /// Vérifier si on fonce dans le mur droit.
    /// </summary>
    /// <returns>Vrai ou faux</returns>
    public bool FonceDansMurDroite()
    {
        bool detecteUneColision;
        float ajustement = 0.1f;
        RaycastHit2D raycastHitRight = Physics2D.Raycast(_collider.bounds.center,
            Vector2.right, _collider.bounds.extents.x + ajustement, LayerMur);

        detecteUneColision = raycastHitRight.collider != null;
        Color rayColor = (detecteUneColision ? Color.red : Color.green);
        Debug.DrawRay(_collider.bounds.center, Vector2.right * (_collider.bounds.extents.x + ajustement), rayColor);

        return detecteUneColision;
    }

}
