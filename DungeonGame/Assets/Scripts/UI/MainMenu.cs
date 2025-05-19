using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject mainFirstSelected;
    [SerializeField] private GameObject settingsFirstSelected;





    private void Start()
    {
      
    }

   

    public void Exit() => Application.Quit();
    public void Play() => SceneManager.LoadScene("DanielScene");
    public void Settings() {
        mainMenu.SetActive(false); 
        settings.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsFirstSelected);

    }
    public void BackMainMenu() { 
        mainMenu.SetActive(true); 
        settings.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainFirstSelected);
    }

}