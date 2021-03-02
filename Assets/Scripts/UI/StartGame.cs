using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private int sceneId;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartGameButton()
    {
        SceneManager.LoadScene(sceneId);
    }
}
