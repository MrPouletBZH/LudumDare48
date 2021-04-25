using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour{
    public AK.Wwise.Event events;
    void Start()    {
        events.Post(gameObject);
    }

}
