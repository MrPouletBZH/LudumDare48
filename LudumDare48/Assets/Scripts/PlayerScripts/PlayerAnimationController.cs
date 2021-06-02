using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationController : MonoBehaviour{

    private Animator animator;
    private bool crouching = false;
    private bool jumping = false; 
    private bool dashing = false; 
    private bool grounded;
    private Vector2 movement;
    private PlayerMovements playerMovements;
    void Start(){
        playerMovements = transform.GetComponent<PlayerMovements>();
        animator = transform.GetComponent<Animator>();
    }

    void Update(){
        movement = playerMovements.getMovement();
        if (!playerMovements.getEnteringScene())
            animator.SetFloat("Speed", Mathf.Abs(movement.x));
        animator.SetFloat("Yvelocity", transform.GetComponent<Rigidbody2D>().velocity.y);
        animator.SetFloat("VerticalSpeed", Mathf.Abs(movement.y));
        animator.SetBool("Dead", playerMovements.getDead());

        if (Dashing() && !dashing) {
            dashing = true;
            animator.SetBool("Dashing", true);
            animator.SetBool("TransiDash", true);
            StartCoroutine(TransiDash());
        }
        else if (!Dashing() && dashing) {
            dashing = false;
            animator.SetBool("Dashing", false);
            animator.SetBool("TransiDash", true);
            StartCoroutine(TransiDash());
        }

        if (Jumping() && !jumping) {
            jumping = true;
            animator.SetBool("Jumping", true);
            animator.SetBool("JumpTransiIn", true);
            StartCoroutine(TransiJumpIn());
        }
        else if (!Jumping() && jumping) {
            jumping = false;
            animator.SetBool("Jumping", false);
            animator.SetBool("JumpTransiOut", true);
            StopCoroutine(TransiJumpIn());
            StartCoroutine(TransiJumpOut());
        }

        if (Crouching() && !crouching) {
            crouching = true;
            animator.SetBool("Crouching",true);
            animator.SetBool("TransiCrouch", true);
            StartCoroutine(TransiCrouch());
        }
        else if (!Crouching() && crouching){
            crouching = false; 
            animator.SetBool("Crouching", false);
            animator.SetBool("TransiCrouch", true);
            StopCoroutine(TransiCrouch());
            StartCoroutine(TransiCrouch());
        }
    }

    private bool Crouching() {
        return (movement.y<-0.5);
    }

    private bool Jumping() {
        return playerMovements.GetIsJumping();
    }

    private bool Dashing(){
        return playerMovements.GetIsDashing();
    }

    private IEnumerator TransiJumpIn(){
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("JumpTransiIn", false);
    }
    private IEnumerator TransiJumpOut(){
        PlayerMovements.landing = true;
        yield return new WaitForSeconds(0.05f);
        PlayerMovements.landing = false;
        yield return new WaitForSeconds(0.05f);
        animator.SetBool("JumpTransiOut", false);
    }
    
    private IEnumerator TransiDash(){
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("TransiDash", false);
    }
    
    private IEnumerator TransiCrouch(){
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("TransiCrouch", false);
    }
}
