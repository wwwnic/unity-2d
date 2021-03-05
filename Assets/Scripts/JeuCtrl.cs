using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contrôleur de jeu.
/// </summary>
public class JeuCtrl : MonoBehaviour
{
    PersoCtrl persoCtrl;
    [SerializeField] GameObject solution;
    ScrollRect solutionScroll;

    private bool _isJumping = false;
    private bool _isAttacking = false;
    private bool _isGrabbing = false;
    private bool _regardeLaSolution = false;
    // Start is called before the first frame update
    void Start()
    {
        persoCtrl = GameObject.FindWithTag("Player").GetComponent<PersoCtrl>();
        solutionScroll = solution.GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        // Mouvement gauche/droite.
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            persoCtrl.Avancer();
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            persoCtrl.Reculer();
        }

        // Sauter.
        if (Input.GetAxisRaw("Jump") != 0)
        {
            if (!_isJumping)
            {
               persoCtrl.SauterDebut();
                _isJumping = true;
            }
            persoCtrl.Sauter();
        }
        else
        {
            persoCtrl.SauterFin();
            _isJumping = false;
        }

        // L'attaque.
        if (Input.GetAxisRaw("Fire1") != 0 && !_regardeLaSolution)
        {
            if (!_isAttacking && !_isGrabbing)
            {
                persoCtrl.Attaquer();
                _isAttacking = true;
            }
        }
        else
        {
            _isAttacking = false;
        }

         // Ouvre ou ferme la solution
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
        {
            _regardeLaSolution = !_regardeLaSolution;
            solution.SetActive(_regardeLaSolution);
        }
    }
}
