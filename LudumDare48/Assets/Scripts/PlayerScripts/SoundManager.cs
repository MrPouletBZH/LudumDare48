using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{
    public AK.Wwise.Event eventDanger;
    public AK.Wwise.Event eventPickup;
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "JumpCollectible"){
            other.gameObject.GetComponent<SoundMaterial>().material.SetValue(gameObject);
            eventPickup.Post(gameObject);
        }
        else if (other.tag == "DashCollectible"){
            other.gameObject.GetComponent<SoundMaterial>().material.SetValue(gameObject);
            eventPickup.Post(gameObject);
        }
        else if (other.tag == "CompletionistCollectible"){
            other.gameObject.GetComponent<SoundMaterial>().material.SetValue(gameObject);
            eventPickup.Post(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "KillingObject") {
            other.gameObject.GetComponent<SoundMaterial>().material.SetValue(gameObject);
            eventDanger.Post(gameObject);
        }
            
    }
}
