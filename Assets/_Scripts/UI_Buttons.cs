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


    public void UICreateDamageText(List<float> informations)
    {
        TextMeshProUGUI damagetext = Instantiate(TextOfDamage, TextOfDamage.transform);
        damagetext.text = informations[0].ToString();
        damagetext.color = Color.red;
        Vector3 position = new Vector3(informations[1] + TextOfDamage.transform.position.x, transform.position.y, informations[2] + TextOfDamage.transform.position.z);
        damagetext.transform.rotation = TextOfDamage.transform.rotation;
        TimeOfSpawn[Time.time] = damagetext;
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<float> info = new List<float>();
        info.Add(50f);
        info.Add(0);
        info.Add(100f);
        UICreateDamageText(info);
        //MenuScreen.SetActive(true);
        //Time.timeScale = 0.0f;
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
