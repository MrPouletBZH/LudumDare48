using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InGameMenu : MonoBehaviour
{
    public string currentScene;
    
    public void Resume(){
        transform.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void ReturnToMenu(){
        SceneManager.LoadScene("MainMenuScene");
        Time.timeScale = 1;
    }
    public void RestartLevel(){
        Time.timeScale = 1;
        SceneManager.LoadScene(currentScene);
    }
}
