using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    private Rigidbody2D rb;
    private new CapsuleCollider2D collider;

    [SerializeField] float speed;

    [SerializeField] private LayerMask layerSol;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        estSurLeSol();
        toucheLeMurDroit();
        toucheLeMurGauche();
    }

    private void FixedUpdate()
    {
        if(toucheLeMurDroit() || toucheLeMurGauche())
        {
            retourner();
            speed = -speed;
        }

        if(!estSurLeSol())
        {
            retourner();
            speed = -speed;
        }
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    public void retourner()
    {
        Vector2 scale = new Vector2(-transform.localScale.x, transform.localScale.y);
        transform.localScale = scale;
    }

    public bool toucheLeMurGauche()
    {
        float ajustement = 0.2f;
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
        float ajustement = 0.2f;
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

    private bool estSurLeSol()
    {
        float ajustement = 0.1f;

        Vector2 centerMin = collider.bounds.center;
        centerMin[0] = collider.bounds.min.x;

        Vector2 centerMax = collider.bounds.center;
        centerMax[0] = collider.bounds.max.x;

        RaycastHit2D raycastHitMax = Physics2D.Raycast(centerMax, Vector2.down, collider.bounds.extents.y + ajustement, layerSol);
        RaycastHit2D raycastHitMin = Physics2D.Raycast(centerMin, Vector2.down, collider.bounds.extents.y + ajustement, layerSol);

        Color rayColor;
        if (raycastHitMax.collider != null && raycastHitMin.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(centerMax, Vector2.down * (collider.bounds.extents.y + ajustement), rayColor);
        Debug.DrawRay(centerMin, Vector2.down * (collider.bounds.extents.y + ajustement), rayColor);

        return raycastHitMax.collider != null && raycastHitMin.collider != null;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "playerAttackHitbox")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "door")
        {
            retourner();
            speed = -speed;
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthSystem>().prendreDamage();
        }
    }
}
