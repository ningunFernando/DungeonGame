using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GamePlayUI : MonoBehaviour
{
    [SerializeField] GameObject hud;
    [SerializeField] GameObject pause;
    [SerializeField] GameObject settings;
    [SerializeField] PlayerInput playerInput;
    [SerializeField]  GameObject mainFirstSelected;
    [SerializeField]  GameObject settingsFirstSelected;


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
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(mainFirstSelected);
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
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(settingsFirstSelected);
        }
        else
        {
            settings.SetActive(false);
            pause.SetActive(true);
            isSettings = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(mainFirstSelected);
        }
       
    }
     void Update()
    {
        if (playerInput.actions["Pause"].WasPressedThisFrame())
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
