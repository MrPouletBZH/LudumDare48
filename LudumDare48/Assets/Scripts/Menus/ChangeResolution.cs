using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeResolution : MonoBehaviour{
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreen;
    List<Resolution> resolutions;
    private List<string> resolutionsString;

    void Start() {
        fullscreen.isOn = Screen.fullScreen;

        resolutions = Screen.resolutions.ToList();

        resolutionDropdown.ClearOptions();
        resolutionsString = new List<string>();
        int currentResolution = -1;

        foreach (Resolution res in resolutions)
            resolutionsString.Add(res.width + " x " + res.height);            
        
        for(int i = 0; i<resolutions.Count-1; i++){
            if (resolutionsString[i] == resolutionsString[i+1]){
                resolutions.Remove(resolutions[i+1]);
                resolutionsString.Remove(resolutionsString[i+1]);
                i--;
            }
        }

        for(int i = 0; i<resolutions.Count; i++)
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolution = i;

        resolutionDropdown.AddOptions(resolutionsString.ToList());
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();
    }

    public void ResolutionChanged(int index){
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);        
    }
    public void ScreenModeChanged(bool fullScreen){
        Screen.fullScreen = fullScreen;
    }
}
