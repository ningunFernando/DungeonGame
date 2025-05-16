using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Dropdown resolutionsDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    
    private Resolution[] commonResolutions = new Resolution[]
    {
        new Resolution { width = 1920, height = 1080 },
        new Resolution { width = 1366, height = 768 },
        new Resolution { width = 1280, height = 720 },
        new Resolution { width = 1024, height = 768 }
    };

    private void Start()
    {
        LoadSettings(); 

        SetupResolutionsDropdown(); 

        if (fullscreenToggle != null)
        {
            bool fullscreen = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;
            fullscreenToggle.isOn = fullscreen;

            fullscreenToggle.onValueChanged.AddListener(SetFullScreen);
        }
    }


    private void SetupResolutionsDropdown()
    {
        if (resolutionsDropdown == null)
        {
            Debug.LogError("Dropdown de resoluciones no asignado en el Inspector!");
            return;
        }

        resolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        int savedWidth = PlayerPrefs.GetInt("ResolutionWidth", Screen.currentResolution.width);
        int savedHeight = PlayerPrefs.GetInt("ResolutionHeight", Screen.currentResolution.height);

        for (int i = 0; i < commonResolutions.Length; i++)
        {
            string option = $"{commonResolutions[i].width} x {commonResolutions[i].height}";
            options.Add(option);

            if (commonResolutions[i].width == savedWidth &&
                commonResolutions[i].height == savedHeight)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();
        resolutionsDropdown.onValueChanged.AddListener(SetResolution);
    }


    public void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex < 0 || resolutionIndex >= commonResolutions.Length)
            return;

        Resolution selectedResolution = commonResolutions[resolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        
        PlayerPrefs.SetInt("ResolutionWidth", selectedResolution.width);
        PlayerPrefs.SetInt("ResolutionHeight", selectedResolution.height);
        PlayerPrefs.Save();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("Fullscreen", isFullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        bool fullscreen = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;
        int width = PlayerPrefs.GetInt("ResolutionWidth", Screen.currentResolution.width);
        int height = PlayerPrefs.GetInt("ResolutionHeight", Screen.currentResolution.height);


        Screen.SetResolution(width, height, fullscreen);
        Screen.fullScreen = fullscreen;
    }

}