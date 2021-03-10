using UnityEngine;

public class VictoireCtrl : MonoBehaviour
{
    private UICtrl _ui;

    void Awake()
    {
        _ui = GameObject.FindWithTag("ui").GetComponent<UICtrl>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            _ui.MontrerEcranVictoire();
        }
    }
}
