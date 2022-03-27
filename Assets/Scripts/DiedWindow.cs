using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class DiedWindow : MonoBehaviour
{
    private float opacity = 1f;
    private TextMeshProUGUI text;
    private TextMeshProUGUI scoreText;
    private Image panel;
    private bool died = false;
    private float delay = 1.6f;

    void Start()
    {
        this.panel = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        this.text = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        this.scoreText = gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
    }
    public void InitAnimation()
    {
        opacity = 0f;
        died = true;
        delay = 2f;
    }

    void FixedUpdate()
    {
        if (opacity < 1) {
            opacity += Time.deltaTime * 0.2f;
            panel.color = new Color(0.12f, 0.12f, 0.12f, opacity);
            text.color = new Color(0.9f, 0f, 0f, opacity);
            scoreText.color = new Color(1f, 1f, 1f, opacity);
        }

        if (died && delay > 0) {
            delay -= Time.deltaTime * 0.2f;
        }

        if (delay <= 0) {
            SceneManager.LoadScene(0);
        }

    }
}
