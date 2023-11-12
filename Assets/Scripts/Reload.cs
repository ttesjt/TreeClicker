using UnityEngine;
using UnityEngine.SceneManagement;


public class Reload : MonoBehaviour
{
    void Update()
    {
    }

    public void ReloadCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
