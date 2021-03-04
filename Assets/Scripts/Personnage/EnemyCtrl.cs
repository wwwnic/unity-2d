using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Classe représentant un enemi du jeu.
/// 
/// Contient les informations de l'ennemi en question comme sa vitesse et la layer sur laquelle il doit faire des vérifications.
/// 
/// La classe permet de faire des vérifications pour s'assurer que l'ennemi peut se déplacer sur une plateforme sans tombé ou bloquer dans une même direction.
/// </summary>
public class EnemyCtrl : MonoBehaviour
{
    private Rigidbody2D rb;
    private new CapsuleCollider2D collider;

    [SerializeField] float speed;

    [SerializeField] private LayerMask layerSol;
    [SerializeField] ForceSystem forceSystem;


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

    /// <summary>
    /// Teste en permanence si une collision avec un mur survient ou si l'ennemi de ne se trouve plus sur le sol.
    /// 
    /// S'il détecte une collision avec un mur ou qu'il ne touche plus le sol, l'ennemi change de côté et le sprite se retourne,
    /// </summary>
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

    /// <summary>
    /// Permet de faire tourner le sprite de l'ennemi qui est appelé lorsqu'une collision avec un mur survient ou lorsqu'il quitte le sol.
    /// </summary>
    public void retourner()
    {
        Vector2 scale = new Vector2(-transform.localScale.x, transform.localScale.y);
        transform.localScale = scale;
    }

    /// <summary>
    /// Teste si l'ennemi entre en collision avec un mur vers la gauche.
    /// </summary>
    /// <returns>
    /// Vrai, s'il détecte une collision vers la gauche.
    /// Faux, s'il ne détecte aucune collision.
    /// </returns>
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

    /// <summary>
    /// Teste si l'ennemi entre en collision avec un mur vers la droite.
    /// </summary>
    /// <returns>
    /// Vrai, s'il détecte une collision vers la droite.
    /// Faux, s'il ne détecte aucune collision.
    /// </returns>
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

    /// <summary>
    /// Permet de tester si les deux extrémités du sprite sont sur le sol.
    /// </summary>
    /// <returns>
    /// Vrai, si un des deux côté ne se trouve plus sur le sol.
    /// Faux, si tous le sprite de l'ennemi se trouve sur le sol.
    /// </returns>
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


    /// <summary>
    /// Méthode qui vérifie si quelque chose entre dans le collider de l'ennemi.
    /// </summary>
    /// <param name="collision">Le collider qui entre dans le collider de l'ennmi.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "playerAttackHitbox")
        {
            forceSystem.SetPlayerForce(1);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Méthode qui vérifie si quelque chose est entré en collision avec l'ennemi et fait la vérification de l'objet avec qui il a eu une collision.
    /// </summary>
    /// <param name="collision">Représente l'objet qui vient d'entrer en collision avec l'ennemi.</param>
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
