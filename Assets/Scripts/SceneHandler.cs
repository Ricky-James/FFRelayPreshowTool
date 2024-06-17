using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2) && SceneManager.GetActiveScene().buildIndex > 0)
        {
	        LoadPreviousScene();
        }
        if (Input.GetKeyDown(KeyCode.F3) && SceneManager.GetActiveScene().buildIndex <= SceneManager.sceneCount)
        {
            LoadNextScene();
        }
    }

    public void LoadPreviousScene()
    {
	    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void LoadNextScene()
    {
	    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Scene restart button on intro UI
    public void ReloadCurrentScene()
    {
	    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
