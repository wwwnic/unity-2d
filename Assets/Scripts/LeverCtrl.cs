using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverCtrl : MonoBehaviour
{

    [SerializeField] SpriteRenderer spriteRendererOn;
    [SerializeField] SpriteRenderer spriteRendererOff;
    [SerializeField] GameObject linkedPedestal;
    [SerializeField] int leverTag;


    private PedestalCtrl pedestal;
    // Start is called before the first frame update
    void Start()
    {
        pedestal = linkedPedestal.GetComponent<PedestalCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ActivatePedestal()
    {
        linkedPedestal.GetComponent<PedestalCtrl>().ActivatePedestal();
    }

    private IEnumerator DeactivateLever()
    {
        yield return new WaitForSeconds(1.0f);

        spriteRendererOn.enabled = false;
        spriteRendererOff.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == GameObject.FindWithTag("playerAttackHitbox").GetComponent<CircleCollider2D>())
        {
            spriteRendererOff.enabled = false;
            spriteRendererOn.enabled = true;
            ActivatePedestal();
            StartCoroutine(DeactivateLever());
        }
    }
}
