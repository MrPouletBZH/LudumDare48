using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParentPlatform : MonoBehaviour
{
    public bool activate;
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player" && activate)
            other.collider.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Player" && activate)
            other.collider.transform.SetParent(null);
    }
}
