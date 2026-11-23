using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Buttons : MonoBehaviour
{
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject MenuScreen;



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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
