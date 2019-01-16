using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GBLib {
	public class TipsController : MonoBehaviour {

		[TextArea(3, 10)]
		public List<string> tips;

		public TextMeshPro CurrentTip;
		public GBButton NextButton;
		public GBButton PrevButton;

		private int Selected;

		// Use this for initialization
		void Start() {
			if (tips.Count <= 1) {
				NextButton.Enabled = false;
				PrevButton.Enabled = false;
			}
			Selected = Random.Range(0, tips.Count);
			ChangeTip();

		}

		void ChangeTip() {
			CurrentTip.text = tips[Selected];
		}

		public void Next() {
			Selected++;
			if (Selected >= tips.Count) {
				Selected = 0;
			}
			ChangeTip();
		}

		public void Previous() {
			Selected--;
			if (Selected < 0) {
				Selected = tips.Count - 1;
			}
			ChangeTip();
		}


	}
}