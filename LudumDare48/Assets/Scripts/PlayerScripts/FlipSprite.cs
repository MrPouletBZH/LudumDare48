using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlipSprite : MonoBehaviour{
    private InputMaster controlsFlipSprite;
    Vector2 movement;
    private SpriteRenderer playerRenderer;
    private bool facingRight = true;
    private PlayerMovements playerMovements;

    void Awake() {
        playerMovements = transform.GetComponent<PlayerMovements>();
    }
    void Update(){

        movement = playerMovements.getMovement();

        if (!playerMovements.GetIsDashing() && Time.timeScale!=0) {

           if (movement.x>0)
                facingRight = true;
            else if (movement.x<0)
                facingRight = false;

            transform.rotation = facingRight? new Quaternion(0, 180, 0, 0) : new Quaternion(0,0,0,0);
        } 
    }

    public bool GetFacingRight() {
        return facingRight;
    }
}
