using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GamePlayUI : MonoBehaviour
{
    [SerializeField] GameObject hud;
    [SerializeField] GameObject pause;
   

    private bool isPaused;
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
            hud.SetActive(true);
            Time.timeScale = 1;

            isPaused = false;
        }
    }
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
}
