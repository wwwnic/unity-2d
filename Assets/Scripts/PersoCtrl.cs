using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersoCtrl : MonoBehaviour
{
    [SerializeField] float vitesse = 2f;

    [SerializeField] float vitesseSautInitiale = 5f;

    [SerializeField] float amortiSaut = 0.1f;

    [SerializeField] LayerMask LayerSol;

    [SerializeField] LayerMask LayerMur;

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
    }


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

    public void Retourner()
    {
        Vector2 scale = new Vector2(-transform.localScale.x, transform.localScale.y);

        transform.localScale = scale;
    }

    public void Attaquer()
    {
        anim.SetTrigger("attaque");
    }

    public void Prendre()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabHitbox.position, Vector2.right * transform.localScale,
           rayDist);
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


    public void SauterDebut()
    {
        if(!_isJumping && EstSurLeSol())
        {
            _isJumping = true;
            _vitesseSaut = vitesseSautInitiale;
        }
    }

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
    public void SauterFin()
    {
        _isJumping = false;
        if (EstSurLeSol())
        {
            anim.SetBool("jumping", false);
        }
    }

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
        Debug.DrawRay(centerLeft,
            Vector2.down * (collider.bounds.extents.y + ajustement), rayColor);
        Debug.DrawRay(centerRight,
            Vector2.down * (collider.bounds.extents.y + ajustement), rayColor);
        Debug.Log(raycastHitCenter.collider);
        Debug.Log(raycastHitLeft.collider);
        Debug.Log(raycastHitRight.collider);

        return raycastHitLeft.collider != null || raycastHitCenter.collider != null || raycastHitRight.collider != null;
    }
    public bool FonceDansMurGauche()
    {
        float ajustement = 0.8f;

        RaycastHit2D raycastHitLeft = Physics2D.Raycast(collider.bounds.center,
    Vector2.left, collider.bounds.extents.x + ajustement, LayerMur);



        Color rayColor;
        if (raycastHitLeft.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(collider.bounds.center,
            Vector2.left * (collider.bounds.extents.y + ajustement), rayColor);
        Debug.Log(raycastHitLeft.collider);

        return raycastHitLeft.collider != null;
    }

    public bool FonceDansMurDroite()
    {
        float ajustement = 0.8f;

        RaycastHit2D raycastHitRight = Physics2D.Raycast(collider.bounds.center,
            Vector2.right, collider.bounds.extents.x + ajustement, LayerMur);



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
        Debug.Log(raycastHitRight.collider);

        return raycastHitRight.collider != null;
    }

}
