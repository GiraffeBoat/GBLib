﻿using System.Collections;
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
	void Start () {
		if (Clips.Length != ClipNames.Length) {
			Debug.LogError ("Missing Audio clip names in SoundController!");
			return;
		} 

		for (int i = 0; i < Clips.Length; i++) {
			AudioSource newSource = gameObject.AddComponent<AudioSource> ();//new AudioSource ();
			newSource.clip = Clips[i];
			newSource.outputAudioMixerGroup = Mixer;
			sources.Add(ClipNames[i], newSource);
		}
	}

	public void PlayClip(string clipName) {
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
		Mixer.audioMixer.SetFloat("Volume", -80f);
	}
	public void UnMute() {
		Mixer.audioMixer.SetFloat("Volume", 0f);
	}
}
