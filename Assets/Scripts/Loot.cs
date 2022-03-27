using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerProperties {
    Speed,
    AttackSpeed,
    Special
}

public class Loot : MonoBehaviour
{
    public PlayerProperties property;
    public float amount;
    private CircleCollider2D collider;
    private AudioSettings audioSettings;

    void Start()
    {
        this.collider = gameObject.GetComponent<CircleCollider2D>();
        GameObject gameSettingsObject = GameObject.Find("GameSettings");
        this.audioSettings = gameSettingsObject.GetComponent<AudioSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.name == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null) {

                Bow bow = other.GetComponent<Bow>();
                
                switch(this.property) {
                    case PlayerProperties.AttackSpeed:
                        if (bow != null) {
                            bow.attackSpeed -= this.amount;
                        }
                        break;
                    case PlayerProperties.Speed:
                        player.speed += this.amount;
                        break;
                    case PlayerProperties.Special:
                        if (bow != null) {
                            bow.specialShots += (int) this.amount;
                        }
                        break;
                }

                audioSettings.PlaySound("Buff");
                Destroy(gameObject);
            }
        }
    }
}
