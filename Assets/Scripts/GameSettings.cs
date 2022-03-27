using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    #region Singleton
    public static GameSettings instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    public bool pause = false;

    public void togglePause() {
        this.pause = !this.pause;
    }

    public void resumeGame() {
        this.pause = false;
    }

    public void pauseGame() {
        this.pause = true;
    }

    public void PlayerDies() {
        this.pause = true;
        GameObject canvas = GameObject.Find("Canvas").gameObject;
        GameObject onDieWindow = canvas.transform.GetChild(2).gameObject;
        onDieWindow.SetActive(true);
        onDieWindow.GetComponent<DiedWindow>().InitAnimation();
    }
 }
