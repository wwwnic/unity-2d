using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnTitle : MonoBehaviour
{

    [SerializeField] private int sceneId;
    public void returnTitle()
    {
        SceneManager.LoadScene(sceneId);
        Time.timeScale = 1;
    }
}
