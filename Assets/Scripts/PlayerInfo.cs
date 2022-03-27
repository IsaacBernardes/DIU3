using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI scoreText;
    public Image specialIcon;
    public Player player;
    public Bow bow;
    public int score;
    

    void Update()
    {
        speedText.text = "SPEED: " + (player.speed * 10).ToString("0.0") + " M/S";
        
        float atckspd = ( 21 - (bow.attackSpeed  * 10));
        if (atckspd > 20)
            atckspd = 20;

        attackSpeedText.text = "ATTACK SPEED: " + atckspd.ToString("0.0") + " APS";
        scoreText.text = "SCORE: " + score;

        if (bow.specialShots > 0) {
            this.specialIcon.color = new Color(0f, 0.3f, 1f, 1f);
        } else {
            this.specialIcon.color = new Color(0f, 0.3f, 1f, 0.4f);
        }
    }
}
