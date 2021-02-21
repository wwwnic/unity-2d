using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCtrl : MonoBehaviour
{

    [SerializeField] Transform grabHitbox;
    [SerializeField] Transform itemHolder;
    [SerializeField] float rayDist;

    private bool _isGrabbing = false;
    private GameObject _grabbedItem;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabHitbox.position, Vector2.right * transform.localScale,
           rayDist);
        if (grabCheck.collider != null && grabCheck.collider.tag == "box")
        {
            _grabbedItem = grabCheck.collider.gameObject;
            if (Input.GetAxisRaw("Fire2") != 0)
            {
                _grabbedItem.transform.parent = itemHolder;
                _grabbedItem.transform.position = itemHolder.position;
                _grabbedItem.GetComponent<Rigidbody2D>().isKinematic = true;
                _isGrabbing = true;
            }
            else
            {
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
