using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public TextMeshProUGUI soundUIReference;
    public TextMeshProUGUI musicUIReference;
    private AudioSettings audioSettings;
    private GameSettings gameSettings;

    void Start() {
        GameObject gameSettingsObject = GameObject.Find("GameSettings");
        audioSettings = gameSettingsObject.GetComponent<AudioSettings>();
        gameSettings = gameSettingsObject.GetComponent<GameSettings>();
    }

    void FixedUpdate() {

        if (soundUIReference != null) {
            soundUIReference.text = ("SOUND: " + audioSettings.soundVolume.ToString());
        }

        if (musicUIReference != null) {
            musicUIReference.text = ("MUSIC: " + audioSettings.musicVolume.ToString());
        }
    }

    public void StartGame() {
        gameSettings.resumeGame();
        audioSettings.PlaySound("Theme");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
