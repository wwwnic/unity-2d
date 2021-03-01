using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{

    [SerializeField] private int sceneId;
    public void restartLevel()
    {
        SceneManager.LoadScene(sceneId);
        Time.timeScale = 1;
    }
}
