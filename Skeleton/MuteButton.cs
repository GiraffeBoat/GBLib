using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GBLib {
	public class MuteButton : MonoBehaviour, ILabelMaster {

		private SoundController Sounds;
		public string BlipSoundName;

		// Use this for initialization
		void Awake() {
			Sounds = ResourceManager.Get().Sounds;
		}

		public void ToggleMute() {
			if (Sounds.IsMuted) {
				Sounds.UnMute();
				if (!BlipSoundName.Equals("")) {
					Sounds.PlayClip(BlipSoundName);
				}
			} else {
				Sounds.Mute();
			}
		}

		public object GetLabelValue(string key) {
			if (key.Equals("muted")) {
				if (Sounds.IsMuted) {
					return "Sound Off";
				} else {
					return "Sound On";
				}
			}
			return "";
		}

		public void SetLabelValue(string key, object value) {
			throw new System.NotImplementedException();
		}
	}
}