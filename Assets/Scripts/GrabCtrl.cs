using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCtrl : MonoBehaviour
{
    // La collision pour détecter un object à prendre.
    [SerializeField] Transform grabHitbox;
    // La position de l'objet tenu.
    [SerializeField] Transform itemHolder;
    // La distance de grabHitbox.
    [SerializeField] float rayDist;

    private bool _isGrabbing = false;
    private GameObject _grabbedItem;

    // Update is called once per frame
    void Update()
    {
        // Trace une ligne à partir de grabHitbox pour détecter un item à prendre.
        RaycastHit2D grabCheck = Physics2D.Raycast(grabHitbox.position, Vector2.right * transform.localScale,
           rayDist);
        if (grabCheck.collider != null && grabCheck.collider.tag == "box")
        {
            _grabbedItem = grabCheck.collider.gameObject;
            if (Input.GetAxisRaw("Fire2") != 0)
            {
                // Prendre l'item
                _grabbedItem.transform.parent = itemHolder;
                _grabbedItem.transform.position = itemHolder.position;
                _grabbedItem.GetComponent<Rigidbody2D>().isKinematic = true;
                _isGrabbing = true;
            }
            else
            {
                // Laisser l'item
                _grabbedItem.transform.parent = null;
                _grabbedItem.GetComponent<Rigidbody2D>().isKinematic = false;
                _isGrabbing = false;
            }
        }
    }

    public bool IsGrabbing()
    {
        return _isGrabbing;
    }
}
