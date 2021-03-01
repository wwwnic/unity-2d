using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrl : MonoBehaviour
{
    [SerializeField]
    GameObject[] winScreenObjects;

    [SerializeField]
    GameObject[] loseScreenObjects;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showWinScreen()
    {
        foreach (GameObject g in winScreenObjects)
        {
            g.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void showLoseScreen()
    {
        foreach (GameObject g in loseScreenObjects)
        {
            g.SetActive(true);
        }
    }
}
