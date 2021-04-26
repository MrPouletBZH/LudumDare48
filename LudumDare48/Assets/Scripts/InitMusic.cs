using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMusic : MonoBehaviour{
    public AK.Wwise.Event initMusic;

    void Awake(){
        initMusic.Post(gameObject);
    }
}
