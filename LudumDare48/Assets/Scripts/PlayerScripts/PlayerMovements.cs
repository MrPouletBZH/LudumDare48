using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public Rigidbody2D rb;
    private Collider2D bxCllder;
    private CapsuleCollider2D plgClldr;
    private Collider2D currentCollider;
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
    private bool isGrounded;
    private bool doubleJumpAllowed;
    private bool isDashing = false;
    private bool dashAllowed = true;
    private bool directionBlocked = false;
    public bool enteringScene = true;
    public static bool landing = false;
    public float crouchingModifier;
    private bool dead = false; 
    private bool itemTaken = false;
    private string success = "";

    public AK.Wwise.Event deathEvent;
    public AK.Wwise.Event jumpEvent;
    public AK.Wwise.Event dashEvent;
    public AK.Wwise.Event cantDashEvent;
    public AK.Wwise.Event cantJumpEvent;
    public AK.Wwise.Event SlideDashEvent;
    public AK.Wwise.Event SlideJumpEvent;

    private GameObject[] dashMovingPlatform;
    private GameObject[] jumpMovingPlatform;
    private GameObject inGameMenu;

    void Awake() {
        inGameMenu = GameObject.FindGameObjectWithTag("InGameMenu");
        inGameMenu.SetActive(false);
        dashMovingPlatform = GameObject.FindGameObjectsWithTag("DashMovingPlatform");
        jumpMovingPlatform = GameObject.FindGameObjectsWithTag("JumpMovingPlatform");
        bxCllder = transform.GetComponent<BoxCollider2D>();
        plgClldr = transform.GetComponent<CapsuleCollider2D>();
        currentCollider = bxCllder;
        plgClldr.enabled = false;
        originalGravityScale = rb.gravityScale;
    }
    void Update(){
        if(!dead){
            if (Input.GetKeyDown(KeyCode.Escape)){
                inGameMenu.SetActive(true);
                Time.timeScale = 0;
            }

            isGrounded = Grounded();

            if(!directionBlocked)
                movement = Input.GetAxisRaw("Horizontal");
            //Dash
            if (!enteringScene && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Backslash)) && dashAllowed && numberOfDash >0 && Time.timeScale!=0) {
                dashEvent.Post(gameObject);
                numberOfDash --;
                isDashing = true;
                dashAllowed = false;
                jumpCounter = 0;
                StartCoroutine(Dash());
            }
            else if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Backslash)) && numberOfDash==0)
                cantDashEvent.Post(gameObject);

            //Jumping
            if (!enteringScene && Input.GetKeyDown(KeyCode.Space) && !isDashing && numberOfJump>0) {
                for(int i =0; i<jumpMovingPlatform.Length; i++){
                    if (i == 0)
                        SlideJumpEvent.Post(gameObject);
                    jumpMovingPlatform[i].GetComponent<DashMovingPlatform>().SwitchingPosition();
                }
                jumpEvent.Post(gameObject);
                numberOfJump --;
                rb.velocity = Vector2.up * jumpSpeed;   
                isJumping =true;
                jumpCounter = jumpTime;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && numberOfJump ==0)
                cantJumpEvent.Post(gameObject);
            
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
        else{
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            movement = 0;
        } 
    }
    void FixedUpdate() {
        if(!enteringScene && !landing) {
            if (Input.GetKey(KeyCode.C) || Input.GetAxisRaw("Vertical")<0)
                transform.position += new Vector3(movement*speed,0,0)*Time.deltaTime*crouchingModifier;
            else
                transform.position += new Vector3(movement*speed,0,0)*Time.deltaTime;
        }
    }

    private bool Grounded() {
        bool retour = Physics2D.BoxCast(currentCollider.bounds.center, currentCollider.bounds.size, 0f, Vector2.down, .013f, platformLayer).collider!=null; 
        if (retour&&rb.velocity.y==0)
            doubleJumpAllowed = true;
        return retour && doubleJumpAllowed;
    }

    private IEnumerator Dash() {
        for(int i =0; i<dashMovingPlatform.Length; i++){
            if (i == 0)
                SlideDashEvent.Post(gameObject);
            dashMovingPlatform[i].GetComponent<DashMovingPlatform>().SwitchingPosition();
        }
        movement = 0;
        rb.gravityScale = 0;
        plgClldr.enabled = true;
        currentCollider = plgClldr;
        bxCllder.enabled = false;
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
        bxCllder.enabled = true;
        currentCollider = bxCllder;
        plgClldr.enabled = false;

        yield return new WaitForSeconds(.02f);
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
            itemTaken = true;
            success = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<Completionist>().ObjectTaken();
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "KillingObject") {
            dead = true;
            StartCoroutine(Death());
        }
            
    }

    private IEnumerator Death(){
    deathEvent.Post(gameObject);
    yield return new WaitForSeconds(0.35f);
    inGameMenu.GetComponent<InGameMenu>().RestartLevel();
    }

    public bool GetIsDashing(){
        return isDashing;
    }

    public bool getDead(){
        return dead;
    }

    public bool GetIsJumping(){
        return !Grounded();
    }

    public bool getEnteringScene(){
        return enteringScene;
    } 
    public bool getIsGrounded(){
        return isGrounded;
    }   
    public string GetSuccess(){
        return success;
    }

    public bool GetItemTaken(){
        return itemTaken;
    }
}
