using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Buttons : MonoBehaviour
{
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject MenuScreen;
    private int ReloadAmount = 1;


    private void RestartSetup()
    {
        SceneManager.CreateScene("TemplateScene");
        SceneManager.GetSceneByName("TemplateScene").Equals(SceneManager.GetActiveScene());
        SceneManager.UnloadSceneAsync("TemplateScene");
    }


    public void PlayButton()
    {
        Time.timeScale = 1.0f;
        MenuScreen.SetActive(false);
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
        SceneManager.CreateScene("ReloadedScene" + ReloadAmount.ToString()).Equals(SceneManager.GetSceneByName("TemplateScene"));
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("ReloadedScene" + ReloadAmount.ToString()));
        ReloadAmount += 1;

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
