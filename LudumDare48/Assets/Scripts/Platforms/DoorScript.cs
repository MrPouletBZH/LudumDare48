using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour{
    private GameObject player;
    private Transform door;
    private Transform target;
    private bool canMove =false;
    public float speed;

    void Start() {
        door = transform.GetChild(0);
        target = transform.GetChild(1);
        player = init.player;
        StartCoroutine(Starting());
    }

    void FixedUpdate() {
        if (canMove)
            door.position = Vector3.MoveTowards(door.position, target.position, speed);
    }

    private IEnumerator Starting(){
        yield return new WaitForSeconds(.3f);
        canMove = true;
        yield return new WaitForSeconds(.3f);
        player.GetComponent<PlayerMovements>().enteringScene = false;
    }


}