using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public float speed = 1f;
    public Rigidbody2D rb;
    private float maxTimeAlive = 1f;

    // Start is called before the first frame update
    void Start() {
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

        Debug.Log(other.name);
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null) {
            enemy.Damage();
            Destroy(gameObject);

        } else {
            Destroy(gameObject);
        }
    }

}
