using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour{
    public bool leftToRight;

    void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.GetComponent<Rigidbody2D>()!= null) {
            int direction = leftToRight? 3:-3;
            other.transform.position += Vector3.right*direction*Time.deltaTime;
        }
    }
}
