using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchSceneManager : MonoBehaviour
{
    #region Singleton
    public static LaunchSceneManager i { get; private set; }
    private void Awake()
    {
        if (i != null && i != this)
        {
            Destroy(gameObject);
            return;
        }
        i = this;
    }
    #endregion

    [SerializeField]
    private string nextSceneName;
    public void NextScene()
    {
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
