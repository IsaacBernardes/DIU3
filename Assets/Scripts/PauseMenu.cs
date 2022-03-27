using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameSettings gameSettings;
    

    void Start() {
        GameObject gameSettingsObject = GameObject.Find("GameSettings");
        gameSettings = gameSettingsObject.GetComponent<GameSettings>();
    }

    // Update is called once per frame
    public void Resume()
    {
        gameSettings.resumeGame();
    }

    public void Pause() {
        gameSettings.pauseGame();
    }

    public void BackToMenu() {
        SceneManager.LoadScene(0);
    }
}
