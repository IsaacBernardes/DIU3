using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public int lifes = 3;
    public float speed = 0.4f;
    public float nextWaypointDistance = 0.1f;
    public GameObject enemyGFX;
    public GameObject drop;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private Seeker seeker;
    private Rigidbody2D rig;
    private bool damage;
    private float damageCooldown = 0.3f;
    private bool died;
    private float diedCooldown = 0.42f;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private GameSettings gameSettings;
    private AudioSettings audioSettings;
    private PlayerInfo playerInfo;

    void Start()
    {
        this.seeker = GetComponent<Seeker>();
        this.rig = GetComponent<Rigidbody2D>();
        this.anim = enemyGFX.GetComponent<Animator>();
        this.spriteRenderer = enemyGFX.GetComponent<SpriteRenderer>();
        GameObject globalGameSettings = GameObject.Find("GameSettings");
        this.gameSettings = globalGameSettings.GetComponent<GameSettings>();
        this.audioSettings = globalGameSettings.GetComponent<AudioSettings>();
        this.playerInfo = GameObject.Find("Canvas/OnRunning").gameObject.GetComponent<PlayerInfo>();

        InvokeRepeating("UpdatePath", 0f, 1f);
    }

    void UpdatePath() {

        if (this.gameSettings.pause)
            return;

        if (seeker.IsDone())
            this.seeker.StartPath(this.rig.position, this.target.position, OnPathComplete);
    }

    void OnPathComplete(Path p) {
        if (!p.error) {
            this.path = p;
            this.currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {   

        if (this.gameSettings.pause) {
            this.rig.velocity = new Vector3(0f, 0f, 0f);
            return;
        }

        if (died == false) {
            if (this.path == null)
                return;
        
            if (this.currentWaypoint >= this.path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            } else {
                reachedEndOfPath = false;
            }


            Vector2 direction = ((Vector2)this.path.vectorPath[this.currentWaypoint] - this.rig.position).normalized;
            Vector2 force = direction * this.speed * Time.deltaTime;
            this.rig.velocity = force;
            float distance = Vector2.Distance(this.rig.position, this.path.vectorPath[this.currentWaypoint]);

            if (distance < this.nextWaypointDistance) {
                currentWaypoint++;
            }


            if (this.rig.velocity.x < 0) {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            } else if (this.rig.velocity.x > 0) {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        } else {
            this.diedCooldown -= Time.deltaTime;
        }

        if (this.diedCooldown <= 0) {
            if (drop != null) {
                Instantiate(drop, gameObject.transform.position, gameObject.transform.rotation);
            }
            this.playerInfo.score += 1;
            Destroy(gameObject);
        }

        if (damage) {
            this.damageCooldown -= Time.deltaTime;
        }

        if (damageCooldown <= 0) {
            this.damage = false;
            this.spriteRenderer.color = new Color(1f, 1f, 1f);
        }
    }

    public void Damage() {
        this.lifes -= 1;

        this.damage = true;
        this.damageCooldown = 0.2f;
        this.spriteRenderer.color = new Color(1f, 0.5f, 0.5f);

        if (lifes <= 0) {
            Die();
        }
    }

    public void Die() {
        this.died = true;
        this.audioSettings.PlaySound("EnemyDie");
        this.spriteRenderer.color = new Color(1f, 1f, 1f);
        this.anim.SetTrigger("Died");
        this.rig.velocity = new Vector3(0f, 0f, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.name == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null) {
                player.Die();
            }
        }
    }
}
