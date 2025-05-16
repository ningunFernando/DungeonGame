using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject mainMenu;

   

    private void Start()
    {
      
    }

   

    public void Exit() => Application.Quit();
    public void Play() => SceneManager.LoadScene("DanielScene");
    public void Settings() {
        mainMenu.SetActive(false); 
        settings.SetActive(true);
    }
    public void BackMainMenu() { 
        mainMenu.SetActive(true); 
        settings.SetActive(false);
    }

}