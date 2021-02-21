﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalCtrl : MonoBehaviour
{
    [SerializeField] int pedestalTag;
    [SerializeField] SpriteRenderer test1;
    [SerializeField] SpriteRenderer test2;
    [SerializeField] GameObject activatedObject1;

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

    public void ActivatePedestal()
    {
        switch(pedestalTag)
        {
            case 1:
                if (_hasRed || _hasBlue)
                {
                    activatedObject1.GetComponent<Animator>().SetTrigger("activated");
                }
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        BoxCtrl boxCtrl = other.gameObject.GetComponent<BoxCtrl>();
        if (boxCtrl)
        {
            if (boxCtrl.getBoxType() == "red")
            {
                _hasRed = true;
                test1.color = Color.white;
            }
            else if (boxCtrl.getBoxType() == "blue")
            {
                _hasBlue = true;
                test2.color = Color.white;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        BoxCtrl boxCtrl = other.gameObject.GetComponent<BoxCtrl>();
        if (boxCtrl)
        {
            if (boxCtrl.getBoxType() == "red")
            {
                _hasRed = false;
                test1.color = Color.black;
            }
            else if (boxCtrl.getBoxType() == "blue")
            {
                _hasBlue = false;
                test2.color = Color.black;
            }
        }
    }
}
