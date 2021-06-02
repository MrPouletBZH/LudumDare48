using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class init : MonoBehaviour
{
    public static GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SceneManager.LoadScene("MainMenuScene");
    }

}
