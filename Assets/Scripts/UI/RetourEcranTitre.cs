using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Retourne a l'ecran titre
/// </summary>
public class RetourEcranTitre : MonoBehaviour
{
    [SerializeField] private int sceneId;

    /// <summary>
    /// Charge une scene qui retourne a l'ecran titre
    /// </summary>
    public void RetourneAEcranTitre()
    {
        SceneManager.LoadScene(sceneId);
        Time.timeScale = 1;
    }
}
