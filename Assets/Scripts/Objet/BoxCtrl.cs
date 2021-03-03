using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script de contrôle des boîtes.
/// </summary>
public class BoxCtrl : MonoBehaviour
{
    // Le type de boîte(Rouge ou bleu).
    [SerializeField] string boxType;

    /// <summary>
    /// Retourne le type de boîte.
    /// </summary>
    /// <returns>"rouge" ou "bleu"</returns>
    public string getBoxType()
    {
        return boxType;
    }
}
