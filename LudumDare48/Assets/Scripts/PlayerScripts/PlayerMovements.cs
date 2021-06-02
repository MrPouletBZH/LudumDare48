using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour{
    private PlayerInput playerInput;

    private Rigidbody2D rb;
    private BoxCollider2D bxCllder;
    private CapsuleCollider2D plgClldr;
    private Collider2D currentCollider;
    private GameObject[] dashMovingPlatform;
    private GameObject[] jumpMovingPlatform;
    private GameObject inGameMenu;
    private Keyboard kb;
    private Gamepad gp;
    private Vector2 movementVector; 
    [SerializeField] private LayerMask platformLayer = new LayerMask();

    [SerializeField] private AK.Wwise.Event deathEvent;
    [SerializeField] private AK.Wwise.Event jumpEvent;
    [SerializeField] private AK.Wwise.Event dashEvent;
    [SerializeField] private AK.Wwise.Event cantDashEvent;
    [SerializeField] private AK.Wwise.Event cantJumpEvent;
    [SerializeField] private AK.Wwise.Event SlideDashEvent;
    [SerializeField] private AK.Wwise.Event SlideJumpEvent;


    private float movement;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float jumpTime;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float crouchingModifier;
    private float jumpCounter;
    private float originalGravityScale;
    private float crouchingModifierPriv = 1;
    private float stillJumping = 0;
    private float deathTimer = 0.35f;
    
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
    private bool dead = false; 
    private bool itemTaken = false;
    private bool gpNotNull;
    private bool startPressed = false;

    private string success = "";

    void Awake() {
        DontDestroyOnLoad(gameObject);
        rb = transform.GetComponent<Rigidbody2D>();
        movementVector = Vector2.zero;
        bxCllder = transform.GetComponent<BoxCollider2D>();
        plgClldr = transform.GetComponent<CapsuleCollider2D>();
        currentCollider = bxCllder;
        plgClldr.enabled = false;
        originalGravityScale = rb.gravityScale;
        kb = InputSystem.GetDevice<Keyboard>();
        gp = InputSystem.GetDevice<Gamepad>(); 
        gpNotNull = gp==null? false: true;

        playerInput = transform.GetComponent<PlayerInput>();
    }
    void Update() {
        if (gpNotNull)
            startPressed = gp.startButton.wasPressedThisFrame;
        if(!dead){
            if ((kb.escapeKey.wasPressedThisFrame || startPressed) && inGameMenu!=null){
                inGameMenu.SetActive(true);
                Time.timeScale = 0;
            }
            isGrounded = Grounded();
            if (stillJumping>0){
                rb.velocity = Vector2.up*jumpSpeed;
                stillJumping-=Time.deltaTime;
            }
        }
        else{
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            movement = 0;
            if (deathTimer == 0.35f){
                deathEvent.Post(gameObject);
            }
            if (deathTimer>0 )
                deathTimer-=Time.deltaTime;
            else {
                dead = false;
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
                deathTimer = 0.35f;
                inGameMenu.GetComponent<InGameMenu>().RestartLevel();
            }
            
        }
    }
    void FixedUpdate() {
        if (!enteringScene && !landing){
            transform.position += new Vector3(movement*speed*crouchingModifierPriv,0,0)*Time.deltaTime;
        }
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
        if (other.gameObject.tag == "KillingObject") Restart();
            
    }

    private void GetValues(){
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Player")){
            if (go!=transform.gameObject){
                transform.position = go.transform.position;

            }
        }
    }

    private bool Grounded() {
        bool retour = Physics2D.BoxCast(currentCollider.bounds.center, currentCollider.bounds.size, 0f, Vector2.down, .013f, platformLayer).collider!=null; 
        if (retour&&rb.velocity.y==0)
            doubleJumpAllowed = true;
        return retour && doubleJumpAllowed;
    }
    private void Jump(){
        if (!dead && !enteringScene && !isDashing && numberOfJump>0 && inGameMenu!=null){
                    stillJumping = 0;
            stillJumping = jumpTime;
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
        else if (!dead && numberOfJump ==0)
            cantJumpEvent.Post(gameObject);
    }
    private void AdaptativeJumpPower(){
        stillJumping = 0;
    }
    private void Dashing(){
        if (!enteringScene && !dead && dashAllowed && numberOfDash >0 && Time.timeScale!=0 && inGameMenu!=null) {
            dashEvent.Post(gameObject);
            numberOfDash --;
            isDashing = true;
            dashAllowed = false;
            jumpCounter = 0;
            StartCoroutine(Dash());
        }
        else if (!dead && numberOfDash==0)
            cantDashEvent.Post(gameObject);
    }
    private void Moving(Vector2 dir){
        movementVector = dir;
        if (dir.x == 0) movement = 0;
        else movement = dir.x<0? -1:1;
        crouchingModifierPriv = dir.y<-0.5? crouchingModifier : 1;
    }
    private void Restart(){
        if (inGameMenu!=null)
            dead = true;
    }

    private IEnumerator Dash() {
        for(int i =0; i<dashMovingPlatform.Length; i++){
            if (i == 0)
                SlideDashEvent.Post(gameObject);
            dashMovingPlatform[i].GetComponent<DashMovingPlatform>().SwitchingPosition();
        }
        float previousMovement = movement;
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
        movement = previousMovement;
        
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
    public bool GetIsDashing(){
        return isDashing;
    }

    public bool getDead(){
        return dead;
    }

    public bool GetIsJumping(){
        return !(isGrounded && rb.velocity.y>-0.01 && rb.velocity.y<0.01);
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

    public Vector2 getMovement(){
        return movementVector;
    }

    public void SetInGameMenu(bool toNull){
        if (!toNull){
            inGameMenu = GameObject.FindGameObjectWithTag("InGameMenu");
            inGameMenu.SetActive(false);
        }
        else inGameMenu = null;
    }

    public void SetMovingPlatforms(){
        dashMovingPlatform = GameObject.FindGameObjectsWithTag("DashMovingPlatform");
        jumpMovingPlatform = GameObject.FindGameObjectsWithTag("JumpMovingPlatform");
    }

    public void ResetVelocity(){
        rb.velocity = Vector2.zero;
        rb.gravityScale = originalGravityScale;
    }

    void OnEnable() {
        playerInput.actions.Enable();
        playerInput.actions["Jump"].performed        += ctx => Jump();
        playerInput.actions["Jump"].canceled         += ctx => AdaptativeJumpPower();
        playerInput.actions["Dash"].performed        += ctx => Dashing();
        playerInput.actions["Movement"].performed    += ctx => Moving(ctx.ReadValue<Vector2>());
        playerInput.actions["Movement"].canceled     += ctx => Moving(Vector2.zero);
        playerInput.actions["MovementBis"].performed += ctx => Moving(ctx.ReadValue<Vector2>());
        playerInput.actions["MovementBis"].canceled  += ctx => Moving(Vector2.zero);
        playerInput.actions["Restart"].performed     += ctx => Restart();
        
        playerInput.actions["JumpGamepad"].performed        += ctx => Jump();
        playerInput.actions["JumpGamepad"].canceled         += ctx => AdaptativeJumpPower();
        playerInput.actions["DashGamepad"].performed        += ctx => Dashing();
        playerInput.actions["MovementGamepad"].performed    += ctx => Moving(ctx.ReadValue<Vector2>());
        playerInput.actions["MovementGamepad"].canceled     += ctx => Moving(Vector2.zero);
        playerInput.actions["MovementGamepadBis"].performed += ctx => Moving(ctx.ReadValue<Vector2>());
        playerInput.actions["MovementGamepadBis"].canceled  += ctx => Moving(Vector2.zero);
        playerInput.actions["RestartGamepad"].performed     += ctx => Restart();
    }

    void OnDisable() {
        playerInput.actions.Disable();
        playerInput.actions["Jump"].performed        -= ctx => Jump();
        playerInput.actions["Jump"].canceled         -= ctx => AdaptativeJumpPower();
        playerInput.actions["Dash"].performed        -= ctx => Dashing();
        playerInput.actions["Movement"].performed    -= ctx => Moving(ctx.ReadValue<Vector2>());
        playerInput.actions["Movement"].canceled     -= ctx => Moving(Vector2.zero);
        playerInput.actions["MovementBis"].performed -= ctx => Moving(ctx.ReadValue<Vector2>());
        playerInput.actions["MovementBis"].canceled  -= ctx => Moving(Vector2.zero);
        playerInput.actions["Restart"].performed     -= ctx => Restart();
                
        playerInput.actions["JumpGamepad"].performed        -= ctx => Jump();
        playerInput.actions["JumpGamepad"].canceled         -= ctx => AdaptativeJumpPower();
        playerInput.actions["DashGamepad"].performed        -= ctx => Dashing();
        playerInput.actions["MovementGamepad"].performed    -= ctx => Moving(ctx.ReadValue<Vector2>());
        playerInput.actions["MovementGamepad"].canceled     -= ctx => Moving(Vector2.zero);
        playerInput.actions["MovementGamepadBis"].performed -= ctx => Moving(ctx.ReadValue<Vector2>());
        playerInput.actions["MovementGamepadBis"].canceled  -= ctx => Moving(Vector2.zero);
        playerInput.actions["RestartGamepad"].performed     -= ctx => Restart();
    }

}
