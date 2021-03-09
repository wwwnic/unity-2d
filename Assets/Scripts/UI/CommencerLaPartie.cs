using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Commence une partie.
/// </summary>
public class CommencerLaPartie : MonoBehaviour
{
    [SerializeField] private int sceneId;

    /// <summary>
    /// Charge une scene
    /// </summary>
    public void CommencerPartie()
    {
        SceneManager.LoadScene(sceneId);
    }
}
