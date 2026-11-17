using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Buttons : MonoBehaviour
{
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject MenuScreen;
    private string CurrentSceneName;


    private void RestartSetup()
    {
        SceneManager.CreateScene("ReloadedScene");
        SceneManager.GetSceneByName("ReloadedScene").Equals(SceneManager.GetActiveScene());
        SceneManager.UnloadSceneAsync("ReloadedScene");
    }


    public void QuitButton()
    {
        Application.Quit();
    }
    public void ResumeButton()
    {
        Time.timeScale = 1.0f;
        PauseScreen.SetActive(false);
    }

    public void BackButton()
    {
        Time.timeScale = 0.0f;
        PauseScreen.SetActive(false);
        MenuScreen.SetActive(true);
    }

    public void ReplayButton()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("ReloadedScene");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("ReloadedScene"));
        CurrentSceneName = SceneManager.GetActiveScene().name;
    }






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RestartSetup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
