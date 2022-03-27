using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    private Rigidbody2D rig;
    public float speed = 0.3f;
    private int horizontalDirection = 0;
    private int verticalDirection = 0;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private bool dashing = false;
    private float dashDuration = 0f;
    private float dashCooldown = 0f;
    private GameSettings gameSettings;
    private AudioSettings audioSettings;
    private bool died = false;

    void Start()
    {
        this.rig = gameObject.GetComponent<Rigidbody2D>();
        this.anim = gameObject.GetComponent<Animator>();
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        GameObject globalGameSettings = GameObject.Find("GameSettings");
        this.gameSettings = globalGameSettings.GetComponent<GameSettings>();
        this.audioSettings = globalGameSettings.GetComponent<AudioSettings>();
    }

    // Update is called once per frame
    void Update()
    {

        if (this.gameSettings.pause || this.died)
            return;

        if (Input.GetKeyDown(KeyCode.Escape)) {
            this.gameSettings.pauseGame();
            this.verticalDirection = 0;
            this.horizontalDirection = 0;

            GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.W) && this.verticalDirection < 1) {
            this.verticalDirection += 1;
        } if (Input.GetKeyDown(KeyCode.A) && this.horizontalDirection > -1) {
            this.horizontalDirection += -1;
        } if (Input.GetKeyDown(KeyCode.S) && this.verticalDirection > -1) {
            this.verticalDirection += -1;
        } if (Input.GetKeyDown(KeyCode.D) && this.horizontalDirection < 1) {
            this.horizontalDirection += 1;
        }


        if (Input.GetKeyUp(KeyCode.W)) {
            this.verticalDirection -= 1;
        } if (Input.GetKeyUp(KeyCode.A)) {
            this.horizontalDirection -= -1;
        } if (Input.GetKeyUp(KeyCode.S)) {
            this.verticalDirection -= -1;
        } if (Input.GetKeyUp(KeyCode.D)) {
            this.horizontalDirection -= 1;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && this.dashCooldown <= 0) {
            this.dashing = true;
            this.dashDuration = 0.2f;
            this.dashCooldown = 1f;
        }

        if (dashDuration <= 0) {
            this.dashing = false;
        }

        this.dashCooldown -= Time.deltaTime;
        this.dashDuration -= Time.deltaTime;

    }

    void FixedUpdate() {
        float realSpeed = speed;

        if (dashing) {
            realSpeed += 2f;
        }

        this.rig.velocity = new Vector2(this.horizontalDirection, this.verticalDirection) * realSpeed;

        this.anim.SetInteger("Horizontal", horizontalDirection);
        this.anim.SetInteger("Vertical", verticalDirection);


        if (horizontalDirection < 0) {
            this.spriteRenderer.flipX = true;
        } else if (horizontalDirection > 0) {
            this.spriteRenderer.flipX = false;
        }

    }


    public void Die() {
        if (died == false) {
            this.died = true;
            this.verticalDirection = 0;
            this.horizontalDirection = 0;
            this.audioSettings.PlaySound("PlayerDeath");  
            this.gameSettings.PlayerDies();
        }
    }
}
