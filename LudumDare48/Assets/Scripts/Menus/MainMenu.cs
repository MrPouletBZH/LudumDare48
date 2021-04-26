using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneToLoad;
    public AK.Wwise.Event okEvent;


    void Awake() {
        if(transform.childCount>1){
            GameObject success = transform.GetChild(2).gameObject;
            if (PlayerPrefs.GetString(sceneToLoad)=="taken")
                success.SetActive(true);
            else
                success.SetActive(false);
        }
    }
    public void LoadLevel(){
        okEvent.Post(gameObject);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Quit(){
        Application.Quit();
    }
}
