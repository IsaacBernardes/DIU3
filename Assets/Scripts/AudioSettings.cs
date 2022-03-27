using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{

    #region Singleton
    public static AudioSettings instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;

            foreach (Sound s in sounds) {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.music ? musicVolume * 0.1f : soundVolume * 0.1f;
                s.source.loop = s.loop;
                s.source.pitch = s.pitch;
            }

            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    [Range(0, 10)]
    public int soundVolume = 10;
    [Range(0, 10)]
    public int musicVolume = 10;
    public Sound[] sounds;

    void Start() {
        PlaySound("Theme");
    }

    public void increaseSoundVolume() {
        if (soundVolume < 10) {
            soundVolume++;
        }
        UpdateSoundsVolumes();
    }

    public void decreaseSoundVolume() {
        if (soundVolume > 0) {
            soundVolume--;
        }
        UpdateSoundsVolumes();
    }

    public void increaseMusicVolume() {
        if (musicVolume < 10) {
            musicVolume++;
        }
        UpdateSoundsVolumes();
    }

    public void decreaseMusicVolume() {
        if (musicVolume > 0) {
            musicVolume--;
        }
        UpdateSoundsVolumes();
    }

    private void UpdateSoundsVolumes() {
        foreach (Sound s in sounds) {
            float newVolume = s.music ? musicVolume : soundVolume;
            s.source.volume = newVolume * 0.1f;
        }
    }

    public void PlaySound (string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null) {
            if (s.music) {
                Sound theme = Array.Find(sounds, sound => sound.name == "Theme");
                theme.source.Stop();
            }
            s.source.Play();
        }
    }


}
