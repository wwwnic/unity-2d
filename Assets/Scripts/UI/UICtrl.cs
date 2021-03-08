using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject[] winScreenObjects;

    [SerializeField]
    private GameObject[] loseScreenObjects;

    [SerializeField]
    private GameObject boutonRestart;

    [SerializeField]
    private GameObject boutonReturnTitle;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        foreach (GameObject g in winScreenObjects)
        {
            g.SetActive(false);
        }

        foreach (GameObject g in loseScreenObjects)
        {
            g.SetActive(false);
        }
    }

    /// <summary>
    /// Montre l'ecran de victoire
    /// </summary>
    public void ShowWinScreen()
    {
        foreach (GameObject g in winScreenObjects)
        {
            g.SetActive(true);
            Time.timeScale = 0;
        }
        boutonRestart.SetActive(false);
        boutonReturnTitle.SetActive(true);
    }


    /// <summary>
    /// Montre l'ecran de defaite.
    /// </summary>
    public void ShowLoseScreen()
    {
        foreach (GameObject g in loseScreenObjects)
        {
            g.SetActive(true);
        }
        boutonRestart.SetActive(false);
        boutonReturnTitle.SetActive(true);
    }
}
