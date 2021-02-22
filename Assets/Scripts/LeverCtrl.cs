using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverCtrl : MonoBehaviour
{
    // Le sprite quand le levier est off.
    [SerializeField] SpriteRenderer spriteRendererOn;
    // Le sprite quand le levier est on.
    [SerializeField] SpriteRenderer spriteRendererOff;
    // Le pédestal relié à ce lever.
    [SerializeField] GameObject linkedPedestal;


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
        GetComponent<BoxCollider2D>().enabled = true;
    }


    //Activer le lever quand le joueur l'attaque.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == GameObject.FindWithTag("playerAttackHitbox").GetComponent<CircleCollider2D>())
        {
            spriteRendererOff.enabled = false;
            spriteRendererOn.enabled = true;
            ActivatePedestal();
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(DeactivateLever());
        }
    }
}
