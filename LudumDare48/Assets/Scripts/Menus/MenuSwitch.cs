using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitch : MonoBehaviour{
    public AK.Wwise.Event okEvent;
    public GameObject settingsMenu;
    public GameObject levelChoicesMenu;

    void Start() {
        settingsMenu.SetActive(false);
        levelChoicesMenu.SetActive(true);
    }

    public void OpenSettings(){
        okEvent.Post(gameObject);
        settingsMenu.SetActive(true);
        levelChoicesMenu.SetActive(false);
    }
    public void OpenLevelChoices(){
        okEvent.Post(gameObject);
        levelChoicesMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
}
