using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Completionist : MonoBehaviour
{
    public string currentLevel;

    public void ObjectTaken(){
        PlayerPrefs.SetString(currentLevel, "taken");
    }
}
