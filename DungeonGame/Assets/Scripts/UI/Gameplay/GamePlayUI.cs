using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GamePlayUI : MonoBehaviour
{
    [SerializeField] GameObject hud;
    [SerializeField] GameObject pause;
    [SerializeField] GameObject settings;


    private bool isPaused;
    private bool isSettings;



    void Start()
    {
        
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");

    }
   

    public void Pause()
    {
        if (!isPaused)
        {
            pause.SetActive(true);
            hud.SetActive(false);
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
           
                pause.SetActive(false);
               settings.SetActive(false);
                hud.SetActive(true);
                Time.timeScale = 1;
                isPaused = false;
            isSettings = false;
            
        }
    }
    public void Settings()
    {
        if(!isSettings)
        {
            settings.SetActive(true);
            pause.SetActive(false);
            isSettings = true;
        }
        else
        {
            settings.SetActive(false);
            pause.SetActive(true);
            isSettings = false;
        }
       
    }
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isSettings)
            {
                Settings();
            }else 
            {
                Pause();
            }

        }
    }
}
