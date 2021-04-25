using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMovingPlatform : MonoBehaviour
{
    private GameObject platform;
    private Transform transform1;
    private Transform transform2;
    private Vector3 currentTarget;
    public float speed;

    void Awake() {
        platform   = transform.GetChild(0).gameObject;
        transform1 = transform.GetChild(1);
        transform2 = transform.GetChild(2);
        currentTarget = transform1.position;
    }

    void FixedUpdate() {
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentTarget, speed);
    }
    public void SwitchingPosition(){
        if(currentTarget == transform1.position)
            currentTarget = transform2.position;
        else
            currentTarget = transform1.position;
    }
    
}
