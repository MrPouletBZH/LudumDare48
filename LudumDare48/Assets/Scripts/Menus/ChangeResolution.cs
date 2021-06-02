using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeResolution : MonoBehaviour{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreen;
    List<Resolution> resolutions;
    private List<string> resolutionsString;
    private int currentHeight;
    private int currentWidth;

    void Start() {
        currentHeight = PlayerPrefs.GetInt("Height");
        currentWidth = PlayerPrefs.GetInt("Width");
        if(currentHeight==0){
            currentHeight = Screen.currentResolution.height;
            currentWidth = Screen.currentResolution.width;
        }
        
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
            if (resolutions[i].width == currentWidth && resolutions[i].height == currentHeight)
                currentResolution = i;

        resolutionDropdown.AddOptions(resolutionsString.ToList());
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();
    }

    public void ResolutionChanged(int index){
        Resolution res = resolutions[index];
        PlayerPrefs.SetInt("Height", res.height);
        PlayerPrefs.SetInt("Width", res.width);
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);        
    }
    public void ScreenModeChanged(bool fullScreen){
        Screen.fullScreen = fullScreen;
    }
}
