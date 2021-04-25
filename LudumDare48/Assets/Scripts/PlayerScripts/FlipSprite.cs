using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSprite : MonoBehaviour
{
private SpriteRenderer playerRenderer;
private bool facingRight = true;

    void Update()
    {
        if (!transform.GetComponent<PlayerMovements>().GetIsDashing() && Time.timeScale!=0) {

           if (Input.GetAxisRaw("Horizontal")>0)
                facingRight = true;
            else if (Input.GetAxisRaw("Horizontal")<0)
                facingRight = false;

            transform.rotation = facingRight? new Quaternion(0, 180, 0, 0) : new Quaternion(0,0,0,0);
        } 
    }

    public bool GetFacingRight() {
        return facingRight;
    }
}
