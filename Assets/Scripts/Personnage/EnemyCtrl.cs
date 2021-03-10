using System.Collections;
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

    [SerializeField] float speed;
    [SerializeField] private LayerMask layerSol;
    [SerializeField] private ForceSystem forceSystem;

    private Rigidbody2D _rb;
    private CapsuleCollider2D _collider;
    private bool _peutTourner;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        _peutTourner = true;
    }

    /// <summary>
    /// Teste en permanence si une collision avec un mur survient ou si l'ennemi de ne se trouve plus sur le sol.
    /// 
    /// S'il détecte une collision avec un mur ou qu'il ne touche plus le sol, l'ennemi change de côté et le sprite se retourne,
    /// </summary>
    private void FixedUpdate()
    {
        if (_peutTourner && ToucheLeMurDroit() | ToucheLeMurGauche())
        {
            Retourner();

        }

        if (_peutTourner && !EstSurLeSol())
        {
            Retourner();

        }
        _rb.velocity = new Vector2(speed, _rb.velocity.y);
    }

    /// <summary>
    /// Permet de faire tourner l'ennemi qui est appelé lorsqu'une collision avec un mur survient ou lorsqu'il quitte le sol.
    /// </summary>
    private void Retourner()
    {
        if (!_peutTourner) return;
        speed = -speed;
        Vector2 scale = new Vector2(-transform.localScale.x, transform.localScale.y);
        transform.localScale = scale;
        StartCoroutine(AttendreApresTourner());
    }


    private IEnumerator AttendreApresTourner()
    {
        _peutTourner = false;
        yield return new WaitForSeconds(2);
        _peutTourner = true;
    }

    /// <summary>
    /// Teste si l'ennemi entre en collision avec un mur vers la gauche avec un raycast et les dessines pour le debug.
    /// </summary>
    /// <returns>
    /// Vrai, s'il détecte une collision vers la gauche.
    /// Faux, s'il ne détecte aucune collision.
    /// </returns>
    private bool ToucheLeMurGauche()
    {
        float ajustement = 0.1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(_collider.bounds.center, Vector2.left, _collider.bounds.extents.x + ajustement, layerSol);

        Color rayColor = (raycastHit.collider != null ? Color.green : Color.red);
        Debug.DrawRay(_collider.bounds.center, Vector2.left * (_collider.bounds.extents.x + ajustement), rayColor);
        return raycastHit.collider != null;
    }

    /// <summary>
    /// Teste si l'ennemi entre en collision avec un mur vers la droite avec un raycast et les dessines pour le debug.
    /// </summary>
    /// <returns>
    /// Vrai, s'il détecte une collision vers la droite.
    /// Faux, s'il ne détecte aucune collision.
    /// </returns>
    private bool ToucheLeMurDroit()
    {
        float ajustement = 0.1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(_collider.bounds.center, Vector2.right, _collider.bounds.extents.x + ajustement, layerSol);

        Color rayColor = (raycastHit.collider != null ? Color.green : Color.red);
        Debug.DrawRay(_collider.bounds.center, Vector2.right * (_collider.bounds.extents.x + ajustement), rayColor);
        return raycastHit.collider != null;
    }

    /// <summary>
    /// Permet de tester si les deux extrémités du sprite sont sur le sol avec un raycast et les dessines pour le debug.
    /// </summary>
    /// <returns>
    /// Vrai, si un des deux côté ne se trouve plus sur le sol.
    /// Faux, si tous le sprite de l'ennemi se trouve sur le sol.
    /// </returns>
    private bool EstSurLeSol()
    {
        float ajustement = 0.1f;

        Vector2 centerMin = _collider.bounds.center;
        centerMin[0] = _collider.bounds.min.x;
        Vector2 centerMax = _collider.bounds.center;
        centerMax[0] = _collider.bounds.max.x;

        RaycastHit2D raycastHitMax = Physics2D.Raycast(centerMax, Vector2.down, _collider.bounds.extents.y + ajustement, layerSol);
        RaycastHit2D raycastHitMin = Physics2D.Raycast(centerMin, Vector2.down, _collider.bounds.extents.y + ajustement, layerSol);

        Color rayColor = (raycastHitMax.collider != null && raycastHitMin.collider != null ? Color.green : Color.red);
        Debug.DrawRay(centerMax, Vector2.down * (_collider.bounds.extents.y + ajustement), rayColor);
        Debug.DrawRay(centerMin, Vector2.down * (_collider.bounds.extents.y + ajustement), rayColor);

        return raycastHitMax.collider != null && raycastHitMin.collider != null;
    }


    /// <summary>
    /// Ajoute de la force au joueur et detruit le gameobject de l'enemie lorsque le joueur l'attaque.
    /// </summary>
    /// <param name="collision">Le collider qui entre dans le collider de l'ennmi.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerAttackHitbox")
        {
            forceSystem.SetPlayerForce(1);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Fait la vérification du colider avec qui il a eu une collision et retourne l'enemie besoin ou fait du degat s'il touche au joueur
    /// </summary>
    /// <param name="collision">Représente le colider qui vient d'entrer en collision avec l'ennemi</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy" && _peutTourner)
        {
            Retourner();
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthSystem>().prendreDamageEtDevientInvicibleSaufSiInvincible();
        }
    }


    /// <summary>
    /// Permet de faire du degat si le joueur reste coller sur un enemie.
    /// </summary>
    /// <param name="collision">Représente le colider qui vient d'entrer en collision avec l'ennemi</param>
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthSystem>().prendreDamageEtDevientInvicibleSaufSiInvincible();
        }
    }

}
