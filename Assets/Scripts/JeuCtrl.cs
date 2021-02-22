using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeuCtrl : MonoBehaviour
{
    PersoCtrl persoCtrl;

    private bool _isJumping = false;
    private bool _isAttacking = false;
    private bool _isGrabbing = false;

    // Start is called before the first frame update
    void Start()
    {
        persoCtrl = GameObject.FindWithTag("Player").GetComponent<PersoCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            persoCtrl.Avancer();
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            persoCtrl.Reculer();
        }

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

        if (Input.GetAxisRaw("Fire1") != 0)
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

        if (Input.GetButtonDown("Fire2"))
        {
            if(!_isGrabbing && !_isAttacking)
            {
                persoCtrl.Prendre();
                _isGrabbing = true;
            }
            else
            {
                persoCtrl.Laisser();
                _isGrabbing = false;
            }
           
        }
    }
}
