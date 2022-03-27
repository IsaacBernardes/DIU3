using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{

    public Transform firePoint;
    public GameObject arrowObject;
    public GameObject specialArrow;
    public float attackSpeed = 0.5f;
    public int specialShots = 1;
    private Animator anim;
    private float attackCooldown = 0f;
    private float attackAnimationCooldown = 0f;
    private bool attacking = false;
    private GameSettings gameSettings;
    private bool readyToShoot = true;
    private bool isSpecial = false;


    void Start() {
        this.anim = gameObject.GetComponent<Animator>();
        GameObject globalGameSettings = GameObject.Find("GameSettings");
        this.gameSettings = globalGameSettings.GetComponent<GameSettings>();
    }

    // Update is called once per frame
    void Update()
    {

        if (this.gameSettings.pause)
            return;

        float horizontalSpeed = this.anim.GetInteger("Horizontal");
        float verticalSpeed = this.anim.GetInteger("Vertical");
        
        if (horizontalSpeed > 0 && !attacking) { // RIGHT
            firePoint.position = gameObject.transform.position;
            firePoint.position += new Vector3(0.1f, -0.05f, 0);
            firePoint.rotation = Quaternion.Euler(0, 0, 270);
        } else if (horizontalSpeed < 0 && !attacking) { // LEFT
            firePoint.position = gameObject.transform.position;
            firePoint.position -= new Vector3(0.1f, +0.05f, 0);
            firePoint.rotation = Quaternion.Euler(0, 0, 90);
        } else if (verticalSpeed > 0 && !attacking) { // TOP
            firePoint.position = gameObject.transform.position;
            firePoint.position += new Vector3(0, 0.12f, 0);
            firePoint.rotation = Quaternion.Euler(0, 0, 0);
        } else if (verticalSpeed < 0 && !attacking) { // BOTTOM
            firePoint.position = gameObject.transform.position;
            firePoint.position -= new Vector3(0, 0.2f, 0);
            firePoint.rotation = Quaternion.Euler(0, 0, 180);
        }

        if (this.attacking == false && this.attackCooldown <= 0 && this.readyToShoot == false) {
            this.readyToShoot = true;
            FindObjectOfType<AudioSettings>().PlaySound("BowReady");
        }

        if (this.readyToShoot) {
            if (Input.GetMouseButtonUp(0)) {
                this.attacking = true;
                this.isSpecial = false;
                this.readyToShoot = false;
                this.anim.SetTrigger("Attacking");
                this.attackAnimationCooldown = 0.2f;
            }
            else if (Input.GetKeyUp(KeyCode.R) && specialShots > 0) {
                this.attacking = true;
                this.isSpecial = true;
                this.specialShots -= 1;
                this.readyToShoot = false;
                this.anim.SetTrigger("Attacking");
                this.attackAnimationCooldown = 0.2f;
            }
        }

        if (this.attacking == true && this.attackAnimationCooldown <= 0) {

            if (!this.isSpecial)
                Shoot();
            else
                SpecialShoot();
        }

        this.attackCooldown -= Time.deltaTime;
        this.attackAnimationCooldown -= Time.deltaTime;
    }

    void Shoot()
    {
        FindObjectOfType<AudioSettings>().PlaySound("Shoot");
        this.attackCooldown = this.attackSpeed;
        this.attacking = false;
        Instantiate(arrowObject, firePoint.position, firePoint.rotation);
    }

    void SpecialShoot()
    {
        FindObjectOfType<AudioSettings>().PlaySound("Special");
        this.attackCooldown = this.attackSpeed;
        this.attacking = false;
        Instantiate(specialArrow, firePoint.position, firePoint.rotation);
    }
}
