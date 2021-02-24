using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    private float speed = 2;
    [SerializeField] private LayerMask layerSol;   

    private bool droit = true;

    private Rigidbody2D rb2d;
    private CapsuleCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        toucheLeSol();
        toucheLeMurDroit();
        toucheLeMurGauche();

        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);

        if (toucheLeSol() == false || toucheLeMurDroit() == true || toucheLeMurGauche() == true)
        {
            if (droit == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                droit = false;
                speed = -2;
            } else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                droit = true;
                speed = -2;
            }
        }
    }

    private bool toucheLeSol()
    {
        float ajustement = 0.02f;

        RaycastHit2D raycastHit = Physics2D.Raycast(collider.bounds.center, Vector2.down, collider.bounds.extents.y + ajustement,layerSol);

        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(collider.bounds.center, Vector2.down * (collider.bounds.extents.y + ajustement), rayColor);
        Debug.Log(raycastHit.collider);

        return raycastHit.collider != null;
    }

    private bool toucheLeMurGauche()
    {
        float ajustement = 0.01f;
        RaycastHit2D raycastHit = Physics2D.Raycast(collider.bounds.center, Vector2.left, collider.bounds.extents.x + ajustement, layerSol);

        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(collider.bounds.center, Vector2.left * (collider.bounds.extents.x + ajustement), rayColor);
        Debug.Log(raycastHit.collider);

        return raycastHit.collider != null;
    }

    private bool toucheLeMurDroit()
    {
        float ajustement = 0.01f;
        RaycastHit2D raycastHit = Physics2D.Raycast(collider.bounds.center, Vector2.right, collider.bounds.extents.x + ajustement, layerSol);

        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(collider.bounds.center, Vector2.right * (collider.bounds.extents.x + ajustement), rayColor);
        Debug.Log(raycastHit.collider);

        return raycastHit.collider != null;
    }
}
