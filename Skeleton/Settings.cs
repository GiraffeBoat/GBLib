using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

    public bool SoundMuted;

    public void Reload() {
        SoundMuted = PlayerPrefs.GetInt("SoundMuted", 0) == 1;
    }

    public void Save() {
        PlayerPrefs.SetInt("SoundMuted", SoundMuted ? 1 : 0);
    }

    public void Mute() {
        SoundMuted = true;
        ResourceManager.Get().Sounds.Mute();
        Save();
    }

    public void UnMute() {
        SoundMuted = false;
        ResourceManager.Get().Sounds.UnMute();
        Save();
    }

    public void ToggleMute() {
        if (SoundMuted) {
            UnMute();
        } else {
            Mute();
        }
    }


}
