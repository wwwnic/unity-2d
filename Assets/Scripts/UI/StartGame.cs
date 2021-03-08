using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Commence une partie.
/// </summary>
public class StartGame : MonoBehaviour
{
    [SerializeField] private int sceneId;

    public void StartGameButton()
    {
        SceneManager.LoadScene(sceneId);
    }
}
