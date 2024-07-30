using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    
    //User Defined Classes
    [Serializable]
    public class AnimationClass{
        public Sprite[] sprites = new Sprite[1];
        public float[] spriteTimer = new float[1];
        public int spriteIndex = 0;

    }

    //User Defined Variables
    public string controls;
    public int movementSpeed = 1;
    public float sprintBoost = 1.5f;
    public int jumpForce = 1000;
    public float jumpBoost = 1.5f;
    public GameObject jumpBoostObj;
    [SerializeField]
    public AnimationClass idle;
    [SerializeField]
    public AnimationClass run;
    public AnimationClass sprint;
    [SerializeField]
    public AnimationClass jump;
    public GameObject pop_effect;
    public GameObject jumpSound;
    public GameObject deathSound;

    //Components
    Rigidbody2D rb;
    SpriteRenderer sr;

    //Variables
    string otherPlayer;
    float sprintBoostAct = 1f;
    bool grounded = false;
    float animationTimer = 0;
    bool running = false;
    bool jumping = false;
    bool falling = false;
    bool elevating = false;
    Vector3 startingPos;
    float jumpBoostTimer = 0;
    float jumpBoostTimerMax = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sprint = run;
        for(int i = 0; i < sprint.spriteTimer.Length; i++){
            sprint.spriteTimer[i] /= sprintBoost;
        }
        if(controls == "artyom"){
            otherPlayer = "neelu";
        }
        else if(controls == "neelu"){
            otherPlayer = "artyom";
        }
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Jump
        if(Input.GetButtonDown(controls + "Jump") && grounded){
            jumping = true;
            rb.AddForce(jumpForce * new Vector3(0, 1));
            GameObject _jump = Instantiate(jumpSound);
        }

        //Move Left and Right
        if(Input.GetButton(controls + "Sprint")){
            sprintBoostAct = sprintBoost;
        }
        else{
            sprintBoostAct = 1f;
        }
        //gameObject.transform.position += movementSpeed * Time.deltaTime * Input.GetAxis(controls + "Movement") * sprintBoostAct * new Vector3(1, 0);
        rb.velocity = new Vector2(movementSpeed * Input.GetAxis(controls + "Movement") * sprintBoostAct, rb.velocity.y);
        if(Input.GetAxis(controls + "Movement") > 0){
            running = true;
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if(Input.GetAxis(controls + "Movement") < 0){
            running = true;
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else{
            running = false;
        }

        //State Manager
        if(rb.velocity.y < 0){
            falling = true;
            elevating = false;
            jumping = false;
        }
        else if(rb.velocity.y > 0){
            elevating = true;
            falling = false;
        }
        else{
            falling = false;
            elevating = false;
        }

        //Animation
        if(falling || elevating || jumping){
            AnimationLooped(jump);
        }
        else if(!(falling || elevating || jumping || running)){ //Idling
            AnimationLooped(idle);
        }
        else if(running && !(falling || elevating || jumping)){ //Running
            AnimationLooped(run);   
        }

        //Check Outbounds
        if(transform.position.y < -100){ //CONSTANT: POSITION BOUNDARY
            Respawn();
        }

        //Timers
        jumpBoostTimer += Time.deltaTime;

    }

    void OnCollisionEnter2D(Collision2D _col){
        if(_col.gameObject.tag == "Ground"){
            grounded = true;
        }
        else if(_col.gameObject.tag == "death"){
            Respawn();
        }
    }

    void OnCollisionStay2D(Collision2D _col){
        if(_col.gameObject.tag == "Ground"){
            grounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D _col){
        if(_col.gameObject.tag == "Ground"){
            grounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D _col){
        if(_col.gameObject.tag == otherPlayer && jumpBoostTimer > jumpBoostTimerMax && (transform.position.y - 0.001f >= _col.gameObject.transform.position.y)){  //CONSTANT: POSITION UNCERTAINTY
            jumpBoostTimer = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(jumpForce * jumpBoost * Vector2.up);
            GameObject _jump = Instantiate(jumpSound);
        }
    }

    void AnimationLooped(AnimationClass anim){
        animationTimer += Time.deltaTime;
        sr.sprite = anim.sprites[anim.spriteIndex];
        if(animationTimer >= anim.spriteTimer[anim.spriteIndex]){
            anim.spriteIndex++;
            animationTimer = 0;
            if(anim.spriteIndex >= anim.sprites.Length){
                anim.spriteIndex = 0;
            }
        }
    }
    
    void Respawn(){
        GameObject _pop1 = Instantiate(pop_effect);
        _pop1.transform.position = gameObject.transform.position;
        transform.position = startingPos;
        rb.velocity = Vector2.zero;
        GameObject _pop2 = Instantiate(pop_effect);
        _pop2.transform.position = gameObject.transform.position;
        GameObject _death = Instantiate(deathSound);
    }
}
