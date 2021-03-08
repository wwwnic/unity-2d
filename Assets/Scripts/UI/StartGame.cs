using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private int sceneId;

    public void StartGameButton()
    {
        SceneManager.LoadScene(sceneId);
    }
}
