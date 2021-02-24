﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalCtrl : MonoBehaviour
{
    // L'identificateur du pédestal.
    [SerializeField] int pedestalTag;
    // Sprites pour tester le pédestal.
    [SerializeField] SpriteRenderer test1;
    [SerializeField] SpriteRenderer test2;
    // L'objet activé par le pédestal/levier.
    [SerializeField] GameObject activatedObject1;
    // Les pédestals additionnels requis pour activer celui-ci
    [SerializeField] List<GameObject> listePedestal;

    private BoxCollider2D boxDetector;

    private bool _hasRed = false;
    private bool _hasBlue= false;
    // Start is called before the first frame update
    void Start()
    {
        boxDetector = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    /// <summary>
    /// Utilisé par le levier pour activer le pédestal selon les items dessus selon l'id de ce dernier.
    /// </summary>
    public void ActivatePedestal()
    {
        switch (pedestalTag)
        {
            case 1:
                if (_hasRed || _hasBlue)
                {
                    activatedObject1.GetComponent<Animator>().SetTrigger("activated");
                }
                break;
            case 2:
                PedestalCtrl pedestal1 = listePedestal[0].GetComponent<PedestalCtrl>();
                if (_hasBlue && _hasRed)
                {
                    if ((pedestal1.HasRed() || pedestal1.HasBlue()) &&
                        !(pedestal1.HasRed() && pedestal1.HasBlue()))
                    {
                        activatedObject1.GetComponent<Animator>().SetTrigger("activated");
                    }
                }
                break;
        }
    }

    public bool HasRed()
    {
        return _hasRed;
    }

    public bool HasBlue()
    {
        return _hasBlue;
    }

    // Détecte les items placés sur le pédestal.
    private void OnTriggerStay2D(Collider2D other)
    {
        BoxCtrl boxCtrl = other.gameObject.GetComponent<BoxCtrl>();
        if (boxCtrl)
        {
            if (boxCtrl.getBoxType() == "red")
            {
                _hasRed = true;
                test1.color = Color.red;
            }
            else if (boxCtrl.getBoxType() == "blue")
            {
                _hasBlue = true;
                test2.color = Color.blue;
            }
        }
    }

    // Détecte quand les items sont retirés du pédestal.
    private void OnTriggerExit2D(Collider2D other)
    {
        BoxCtrl boxCtrl = other.gameObject.GetComponent<BoxCtrl>();
        if (boxCtrl)
        {
            if (boxCtrl.getBoxType() == "red")
            {
                _hasRed = false;
                test1.color = new Color(0.3537736f, 0.822655f, 1.0f);
            }
            else if (boxCtrl.getBoxType() == "blue")
            {
                _hasBlue = false;
                test2.color = new Color(0.3537736f, 0.822655f, 1.0f);
            }
        }
    }

}
