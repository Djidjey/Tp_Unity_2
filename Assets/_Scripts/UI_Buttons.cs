using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_Buttons : MonoBehaviour
{
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject MenuScreen;
    [SerializeField] private TextMeshProUGUI TextOfDamage;
    private Dictionary<float, TextMeshProUGUI > TimeOfSpawn = new Dictionary<float, TextMeshProUGUI>();
    private float DamageTextVisibleTime = 5;



    public void PlayButton()
    {
        SceneManager.LoadScene(0);
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


    public void UICreateDamageText(List<float> informations)
    {
        TextMeshProUGUI damagetext = Instantiate(TextOfDamage, TextOfDamage.transform);
        damagetext.maxVisibleCharacters = informations[0].ToString().Length;
        damagetext.text = informations[0].ToString();
        damagetext.color = Color.red;
        Vector3 position = new Vector3(informations[1], transform.position.y, informations[2]);
        damagetext.transform.position = position;
        damagetext.transform.rotation = TextOfDamage.transform.rotation;
        TimeOfSpawn[Time.time] = damagetext;
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MenuScreen.SetActive(true);
        TextOfDamage.maxVisibleCharacters = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeOfSpawn.Count > 0)
        {
            foreach (float time in TimeOfSpawn.Keys)
            {
                if (time + DamageTextVisibleTime < Time.time)
                {
                    Destroy(TimeOfSpawn[time]);
                }
                else
                { break; }
            }
        }
    }
}
