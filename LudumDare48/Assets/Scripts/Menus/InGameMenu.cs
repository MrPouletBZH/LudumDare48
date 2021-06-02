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
        okEvent.Post(gameObject);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovements>().SetInGameMenu(true);
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
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
    public void NextLevel(string nextLevel){
        Time.timeScale = 1;
        SceneManager.LoadScene(nextLevel);    
    }
}
