using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GBLib {
	public class TopScore : MonoBehaviour {
		public TextMeshPro RankText;
		public TextMeshPro ValueText;

		private TextBlinker Blinks;

		void Awake() {
			Blinks = GetComponent<TextBlinker>();
		}

		public void Set(int rank, int value, bool blink = false) {
			RankText.text = string.Format("{0}.", rank);
			ValueText.text = value.ToString("N0");
			if (blink && Blinks != null) {
				Blinks.Active = true;
			}
		}
	}
}