using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public Rigidbody2D rb;
    private BoxCollider2D bxCllder;
    [SerializeField] private LayerMask platformLayer = new LayerMask();

    public static float movement;
    public float speed;
    public float jumpSpeed;
    public float jumpTime;
    public float dashDuration;
    public float dashSpeed;
    private float jumpCounter;
    private float originalGravityScale;
    
    public int numberOfDash;
    public int numberOfJump;  

    private bool isJumping;
    private bool hangingOnWall;
    private bool isGrounded;
    private bool doubleJumpAllowed;
    private bool isDashing = false;
    private bool dashAllowed = true;
    private bool directionBlocked = false;
    public bool onMenu = true;
    public static bool onDialogue = false;
    public static bool landing = false;

    private GameObject[] dashMovingPlatform;
    private GameObject[] jumpMovingPlatform;
    private GameObject inGameMenu;

    void Awake() {
        inGameMenu = GameObject.FindGameObjectWithTag("InGameMenu");
        inGameMenu.SetActive(false);
        dashMovingPlatform = GameObject.FindGameObjectsWithTag("DashMovingPlatform");
        jumpMovingPlatform = GameObject.FindGameObjectsWithTag("JumpMovingPlatform");
        bxCllder = transform.GetComponent<BoxCollider2D>();
        originalGravityScale = rb.gravityScale;
    }
    void Update(){

        if (Input.GetKeyDown(KeyCode.Escape)){
            inGameMenu.SetActive(true);
            Time.timeScale = 0;
        }

        isGrounded = Grounded();

        if(!directionBlocked)
            movement = Input.GetAxisRaw("Horizontal");
        //Dash
        if (!onMenu && !onDialogue && Input.GetKeyDown(KeyCode.LeftShift)&& dashAllowed && numberOfDash >0 && Time.timeScale!=0) {
            numberOfDash --;
            isDashing = true;
            dashAllowed = false;
            jumpCounter = 0;
            StartCoroutine(Dash());
        }
        //Jumping
        if (!onMenu && !onDialogue && Input.GetKeyDown(KeyCode.Space) && !isDashing && (isGrounded || hangingOnWall ||doubleJumpAllowed) && numberOfJump>0) {
            foreach(GameObject go in jumpMovingPlatform)
                go.GetComponent<DashMovingPlatform>().SwitchingPosition();
            numberOfJump --;
            rb.velocity = Vector2.up * jumpSpeed;   
            isJumping =true;
            jumpCounter = jumpTime;
        }
        
        //Adaptative jump power
        if (Input.GetKey(KeyCode.Space) && isJumping)
            if (jumpCounter >0 ) {
                rb.velocity = Vector2.up * jumpSpeed;
                jumpCounter-=Time.deltaTime;
            }
            else 
                isJumping = false;
        if (Input.GetKeyUp(KeyCode.Space))
            isJumping = false;    
    }
    void FixedUpdate() {
        if(!onMenu && !onDialogue && !landing)
            transform.position += new Vector3(movement*speed,0,0)*Time.deltaTime;
    }

    private bool Grounded() {
        bool retour = Physics2D.BoxCast(bxCllder.bounds.center, bxCllder.bounds.size, 0f, Vector2.down, .01f, platformLayer).collider!=null; 
        if (retour&&rb.velocity.y==0)
            doubleJumpAllowed = true;
        return retour && doubleJumpAllowed;
    }

    private IEnumerator Dash() {
        foreach(GameObject go in dashMovingPlatform)
            go.GetComponent<DashMovingPlatform>().SwitchingPosition();
        movement = 0;
        rb.gravityScale = 0;
        int direction = transform.GetComponent<FlipSprite>().GetFacingRight()? 1: -1;
        Quaternion baseRotation = transform.rotation;
        Vector2 previousVelocity = new Vector2(rb.velocity.x, 0);
        
        directionBlocked = true;
        rb.velocity = new Vector2(dashSpeed, 0)*direction;
        yield return new WaitForSeconds(dashDuration);
        
        rb.gravityScale = originalGravityScale;
        transform.rotation = baseRotation;
        rb.velocity = previousVelocity;
        directionBlocked = false;
        isDashing = false;
        
        yield return new WaitForSeconds(.5f);
        dashAllowed = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "JumpCollectible"){
            Destroy(other.gameObject);
            numberOfJump++;
        }
        else if (other.tag == "DashCollectible"){
            Destroy(other.gameObject);
            numberOfDash++;
        }
        else if (other.tag == "CompletionistCollectible"){
            Destroy(other.gameObject);
            GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<Completionist>().ObjectTaken();
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "KillingObject") {
            inGameMenu.GetComponent<InGameMenu>().RestartLevel();
        }
            
    }

    public bool GetIsDashing(){
        return isDashing;
    }

    public bool GetIsJumping(){
        return !Grounded();
    }

    public bool GetClimbing(){
        return hangingOnWall;
    }
    
}
