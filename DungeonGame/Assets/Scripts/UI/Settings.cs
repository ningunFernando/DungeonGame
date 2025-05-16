using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Dropdown resolutionsDropdown;
    private Resolution[] commonResolutions = new Resolution[]
       {
        new Resolution { width = 1920, height = 1080 },
        new Resolution { width = 1366, height = 768 },
        new Resolution { width = 1280, height = 720 },
        new Resolution { width = 1024, height = 768 }
       };

    void Start()
    {
        if (resolutionsDropdown == null)
        {
            Debug.LogError("Dropdown de resoluciones no asignado en el Inspector!");
            return;
        }

        resolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < commonResolutions.Length; i++)
        {
            string option = $"{commonResolutions[i].width} x {commonResolutions[i].height}";
            options.Add(option);

            if (commonResolutions[i].width == Screen.currentResolution.width &&
                commonResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution selectedResolution = commonResolutions[resolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }
    public void SetFullScreen(bool isFullScreen) => Screen.fullScreen = isFullScreen;
  
}
