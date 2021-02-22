using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersoCtrl : MonoBehaviour
{
    [SerializeField] float vitesse = 4f;

    [SerializeField] float vitesseSautInitiale = 4.5f;

    [SerializeField] float amortiSaut = 2f;

    [SerializeField] LayerMask LayerSol;


    Rigidbody2D rb;
    Animator anim;
    CapsuleCollider2D collider;

    bool regarderDroite = true;

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
        rb.velocity = new Vector2(vitesse, rb.velocity.y);
        if (!regarderDroite)
        {
            regarderDroite = true;
            Retourner();
        }
    }

    public void Reculer()
    {

        rb.velocity = new Vector2(-vitesse, rb.velocity.y);
        if (regarderDroite)
        {
            regarderDroite = false;
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

    public void SauterDebut()
    {
        if(!_isJumping)
        {
            _isJumping = true;
            _vitesseSaut = vitesseSautInitiale;
        }
    }

    public void Sauter()
    {
        if(_isJumping && EstSurLeSol())
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
    }
    public void SauterFin()
    {
        _isJumping = false;
        if (EstSurLeSol())
        {
            anim.SetBool("jumping", false);
            anim.SetBool("estSurSol", true);

        }
    }

    private bool EstSurLeSol()
    {
        float ajustement = 0.08f;
        RaycastHit2D raycastHit = Physics2D.Raycast(collider.bounds.center,
            Vector2.down, collider.bounds.extents.y + ajustement,LayerSol);

        Color rayColor;
        if (raycastHit.collider != null)
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

        return raycastHit.collider != null;
    }


    public bool FonceDansMurGauche()
    {
        float ajustement = 1.5f;

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
    Vector2.down * (collider.bounds.extents.y + ajustement), rayColor);
        Debug.Log(raycastHitLeft.collider);

        return raycastHitLeft.collider != null;
    }


    public bool FonceDansMurDroite()
    {
        float ajustement = 1.5f;

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
    Vector2.down * (collider.bounds.extents.y + ajustement), rayColor);
        Debug.Log(raycastHitRight.collider);

        return raycastHitRight.collider != null;
    }

}
