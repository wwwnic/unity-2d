using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script de contrôle des leviers.
/// </summary>
public class LeverCtrl : MonoBehaviour
{
    // Le sprite quand le levier est off.
    [SerializeField] SpriteRenderer spriteRendererOn;
    // Le sprite quand le levier est on.
    [SerializeField] SpriteRenderer spriteRendererOff;
    // Le pédestal relié à ce lever.
    [SerializeField] GameObject linkedPedestal;

    /// <summary>
    /// Active le pédestal relié.
    /// </summary>
    private void ActivatePedestal()
    {
        linkedPedestal.GetComponent<PedestalCtrl>().ActivatePedestal();
    }

    /// <summary>
    /// Désactive le levier après une seconde.
    /// </summary>
    /// <returns>Énumérateur</returns>
    private IEnumerator DeactivateLever()
    {
        yield return new WaitForSeconds(1.0f);

        spriteRendererOn.enabled = false;
        spriteRendererOff.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }


    /// <summary>
    /// Active le levier quand le joueur l'attaque.
    /// </summary>
    /// <param name="other">La collision de l'attaque du joueur.(Qui est un Trigger)</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            spriteRendererOff.enabled = false;
            spriteRendererOn.enabled = true;
            ActivatePedestal();
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(DeactivateLever());
        }    
    }
}
