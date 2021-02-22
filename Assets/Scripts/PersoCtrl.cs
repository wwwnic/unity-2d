using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersoCtrl : MonoBehaviour
{
    [SerializeField] float vitesse = 2f;

    [SerializeField] float vitesseSautInitiale = 5f;

    [SerializeField] float amortiSaut = 0.1f;

    [SerializeField] LayerMask LayerSol;

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
}
