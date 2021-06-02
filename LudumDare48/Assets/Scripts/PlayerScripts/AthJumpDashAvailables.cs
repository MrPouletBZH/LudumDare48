using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AthJumpDashAvailables : MonoBehaviour{

    private PlayerMovements player;
    private GameObject dashAvailables;
    private GameObject jumpAvailables;
    void Start() {
        player = init.player.GetComponent<PlayerMovements>();
        dashAvailables = transform.GetChild(2).gameObject;
        jumpAvailables = transform.GetChild(1).gameObject;
    }
    void Update() {
        switch (player.numberOfDash){
            case 0:
                dashAvailables.transform.GetChild(0).gameObject.SetActive(false);
                dashAvailables.transform.GetChild(1).gameObject.SetActive(false);
                dashAvailables.transform.GetChild(2).gameObject.SetActive(false);
                dashAvailables.transform.GetChild(3).gameObject.SetActive(false);
                dashAvailables.transform.GetChild(4).gameObject.SetActive(false);
                break;
            case 1: 
                dashAvailables.transform.GetChild(0).gameObject.SetActive(true);
                dashAvailables.transform.GetChild(1).gameObject.SetActive(false);
                dashAvailables.transform.GetChild(2).gameObject.SetActive(false);
                dashAvailables.transform.GetChild(3).gameObject.SetActive(false);
                dashAvailables.transform.GetChild(4).gameObject.SetActive(false);
                break;
            case 2:
                dashAvailables.transform.GetChild(0).gameObject.SetActive(true);
                dashAvailables.transform.GetChild(1).gameObject.SetActive(true);
                dashAvailables.transform.GetChild(2).gameObject.SetActive(false);
                dashAvailables.transform.GetChild(3).gameObject.SetActive(false);
                dashAvailables.transform.GetChild(4).gameObject.SetActive(false); 
                break;
            case 3: 
                dashAvailables.transform.GetChild(0).gameObject.SetActive(true);
                dashAvailables.transform.GetChild(1).gameObject.SetActive(true);
                dashAvailables.transform.GetChild(2).gameObject.SetActive(true);
                dashAvailables.transform.GetChild(3).gameObject.SetActive(false);
                dashAvailables.transform.GetChild(4).gameObject.SetActive(false);
                break;
            case 4: 
                dashAvailables.transform.GetChild(0).gameObject.SetActive(true);
                dashAvailables.transform.GetChild(1).gameObject.SetActive(true);
                dashAvailables.transform.GetChild(2).gameObject.SetActive(true);
                dashAvailables.transform.GetChild(3).gameObject.SetActive(true);
                dashAvailables.transform.GetChild(4).gameObject.SetActive(false);
                break;
            default:
                dashAvailables.transform.GetChild(0).gameObject.SetActive(true);
                dashAvailables.transform.GetChild(1).gameObject.SetActive(true);
                dashAvailables.transform.GetChild(2).gameObject.SetActive(true);
                dashAvailables.transform.GetChild(3).gameObject.SetActive(true);
                dashAvailables.transform.GetChild(4).gameObject.SetActive(true);
                break;
        }

    switch (player.numberOfJump){
            case 0:
                jumpAvailables.transform.GetChild(0).gameObject.SetActive(false);
                jumpAvailables.transform.GetChild(1).gameObject.SetActive(false);
                jumpAvailables.transform.GetChild(2).gameObject.SetActive(false);
                jumpAvailables.transform.GetChild(3).gameObject.SetActive(false);
                jumpAvailables.transform.GetChild(4).gameObject.SetActive(false);
                break;
            case 1: 
                jumpAvailables.transform.GetChild(0).gameObject.SetActive(true);
                jumpAvailables.transform.GetChild(1).gameObject.SetActive(false);
                jumpAvailables.transform.GetChild(2).gameObject.SetActive(false);
                jumpAvailables.transform.GetChild(3).gameObject.SetActive(false);
                jumpAvailables.transform.GetChild(4).gameObject.SetActive(false);
                break;
            case 2:
                jumpAvailables.transform.GetChild(0).gameObject.SetActive(true);
                jumpAvailables.transform.GetChild(1).gameObject.SetActive(true);
                jumpAvailables.transform.GetChild(2).gameObject.SetActive(false);
                jumpAvailables.transform.GetChild(3).gameObject.SetActive(false);
                jumpAvailables.transform.GetChild(4).gameObject.SetActive(false); 
                break;
            case 3: 
                jumpAvailables.transform.GetChild(0).gameObject.SetActive(true);
                jumpAvailables.transform.GetChild(1).gameObject.SetActive(true);
                jumpAvailables.transform.GetChild(2).gameObject.SetActive(true);
                jumpAvailables.transform.GetChild(3).gameObject.SetActive(false);
                jumpAvailables.transform.GetChild(4).gameObject.SetActive(false);
                break;
            case 4: 
                jumpAvailables.transform.GetChild(0).gameObject.SetActive(true);
                jumpAvailables.transform.GetChild(1).gameObject.SetActive(true);
                jumpAvailables.transform.GetChild(2).gameObject.SetActive(true);
                jumpAvailables.transform.GetChild(3).gameObject.SetActive(true);
                jumpAvailables.transform.GetChild(4).gameObject.SetActive(false);
                break;
            default:
                jumpAvailables.transform.GetChild(0).gameObject.SetActive(true);
                jumpAvailables.transform.GetChild(1).gameObject.SetActive(true);
                jumpAvailables.transform.GetChild(2).gameObject.SetActive(true);
                jumpAvailables.transform.GetChild(3).gameObject.SetActive(true);
                jumpAvailables.transform.GetChild(4).gameObject.SetActive(true);
                break;
        }
         
    }
}
