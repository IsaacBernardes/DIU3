using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : MonoBehaviour
{

    public float speed = 1f;
    private Rigidbody2D rb;
    private float maxTimeAlive = 1f;

    // Start is called before the first frame update
    void Start() {
        this.rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    void Update() {

        if (maxTimeAlive <= 0) {
            Destroy(gameObject);
        }

        this.maxTimeAlive -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.name == "Player")
        {
            return;
        }

        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null) {
            enemy.Die();
        }
    }

}
