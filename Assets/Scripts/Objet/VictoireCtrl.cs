using UnityEngine;

public class VictoireCtrl : MonoBehaviour
{
    UICtrl _ui;

    void Start()
    {
        _ui = GameObject.FindWithTag("ui").GetComponent<UICtrl>();
    }

    public void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _ui.showWinScreen();
        }
    }
}
