using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Retourne a l'ecran titre
/// </summary>
public class ReturnTitle : MonoBehaviour
{

    [SerializeField] private int sceneId;
    public void ReturnTitleScreen()
    {
        SceneManager.LoadScene(sceneId);
        Time.timeScale = 1;
    }
}
