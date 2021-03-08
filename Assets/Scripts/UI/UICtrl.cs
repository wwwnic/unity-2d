using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrl : MonoBehaviour
{
    [SerializeField]
    GameObject[] winScreenObjects;

    [SerializeField]
    GameObject[] loseScreenObjects;

    [SerializeField]
    GameObject boutonRestart;

    [SerializeField]
    GameObject boutonReturnTitle;

    [SerializeField]
    Grid bgUI;

    Vector2 positionCamera;

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
        bgUI.transform.position = new Vector2(-25.6f, -14.6f);
    }

    private void Update()
    {
        positionCamera = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
    }

    public void ShowWinScreen()
    {
        foreach (GameObject g in winScreenObjects)
        {
            g.SetActive(true);
            Time.timeScale = 0;
        }
        boutonRestart.SetActive(false);
        boutonReturnTitle.SetActive(true);
        bgUI.transform.position = positionCamera;
    }

    public void ShowLoseScreen()
    {
        foreach (GameObject g in loseScreenObjects)
        {
            g.SetActive(true);
        }
        boutonRestart.SetActive(false);
        boutonReturnTitle.SetActive(true);
        bgUI.transform.position = positionCamera;
    }
}
