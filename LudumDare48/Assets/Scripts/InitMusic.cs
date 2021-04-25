using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMusic : MonoBehaviour{
    public AK.Wwise.Event events;
    void Awake(){
        events.Post(gameObject);
        //DontDestroyOnLoad(gameObject);
    }
}
