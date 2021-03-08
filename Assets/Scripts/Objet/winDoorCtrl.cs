using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winDoorCtrl : MonoBehaviour
{
    UICtrl ui;


    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.FindWithTag("ui").GetComponent<UICtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ui.ShowWinScreen();
        }
    }
}
