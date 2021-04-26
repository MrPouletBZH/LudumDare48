using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InGameMenu : MonoBehaviour
{
    public string currentScene;
    public AK.Wwise.Event backEvent;
    public AK.Wwise.Event okEvent;
    
    public void Resume(){
        transform.gameObject.SetActive(false);
        backEvent.Post(gameObject);
        Time.timeScale = 1;
    }
    public void ReturnToMenu(){
        SceneManager.LoadScene("MainMenuScene");
        okEvent.Post(gameObject);
        Time.timeScale = 1;
    }
    public void RestartLevelButton(){
        okEvent.Post(gameObject);
        RestartLevel();
    }
    public void RestartLevel(){
        Time.timeScale = 1;
        SceneManager.LoadScene(currentScene);
    }
    public void Quit(){
        Application.Quit();
    }
}
