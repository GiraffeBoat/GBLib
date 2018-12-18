using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour {

	public AudioMixerGroup Mixer;

	public AudioClip[] Clips;

	public string[] ClipNames;

	private Dictionary<string, AudioSource> sources = new Dictionary<string, AudioSource> ();

	private bool Muted = false;

	// Use this for initialization
	void Awake () {
		if (Clips.Length != ClipNames.Length) {
			Debug.LogError ("Missing Audio clip names in SoundController!");
			return;
		} 

		for (int i = 0; i < Clips.Length; i++) {
			AudioSource newSource = gameObject.AddComponent<AudioSource> ();//new AudioSource ();
			newSource.clip = Clips[i];
			newSource.outputAudioMixerGroup = Mixer;
			sources.Add(ClipNames[i], newSource);
            //Debug.Log("adding clip " + ClipNames[i]);
		}
	}

	public void PlayClip(string clipName) {
		if (Muted) {
			return;
		}
		if (sources.ContainsKey (clipName)) {
			AudioSource source = sources [clipName];
			if (source != null) {
				source.Play ();
			} else {
				Debug.LogWarning ("Error with sound clip " + clipName);
			}
		} else {
			Debug.LogWarning ("Nonexistent sound clip " + clipName);
		}
	}

	public void Mute() {
		Muted = true;
		//Mixer.audioMixer.SetFloat("Volume", -80f);
	}
	public void UnMute() {
		Muted = false;
		//Mixer.audioMixer.SetFloat("Volume", 0f);
	}
}
