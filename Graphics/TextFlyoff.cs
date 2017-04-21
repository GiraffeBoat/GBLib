using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GBLib {
	[RequireComponent(typeof(Text))]
	public class TextFlyoff : MonoBehaviour {

		private static float RegLiftTime = .3f;
		private static float RegFloatTime = .4f;
		private static float RegScale = 1f;
		private static float CritLiftTime = .3f;
		private static float CritFloatTime = .5f;
		private static float CritScale = 1.5f;
		private static float LiftHeight = 1f;

		[SerializeField]
		private Color HealColor;
		[SerializeField]
		private Color HitColor;
		[SerializeField]
		private Color CritColor;

		private Text text;

		// Use this for initialization
		void Start () {
			text = GetComponent<Text> ();
		}

		public void Begin(Vector3 position, string textValue, int severity) {
			if (text == null) {
				text = GetComponent<Text> ();
			}
			text.text = textValue;
			transform.position = position;
			float liftTime = 0;
			float floatTime = 0;
			float scale = 0;

			switch (severity) {
			case 0:
				text.color = HealColor;
				liftTime = RegLiftTime;
				floatTime = RegFloatTime;
				scale = RegScale;
				break;
			case 1:
				text.color = HitColor;
				liftTime = RegLiftTime;
				floatTime = RegFloatTime;
				scale = RegScale;
				break;
			case 2:
				text.color = HitColor;
				liftTime = RegLiftTime;
				floatTime = RegFloatTime;
				scale = RegScale;
				break;
			case 3:
				text.color = CritColor;
				liftTime = CritLiftTime;
				floatTime = CritFloatTime;
				scale = CritScale;
				break;
			}

			StartCoroutine (AnimateRoutine (liftTime, floatTime, scale));
		}

		public IEnumerator AnimateRoutine(float liftTime, float floatTime, float scale) {
			float timer = 0;
			Vector3 startPosition = transform.position;
			Vector3 floatSpot = startPosition + Vector3.up * LiftHeight;

			while (timer < liftTime) {
				timer += Time.deltaTime;
				transform.position = Vector3.Lerp (startPosition, floatSpot, timer / liftTime);
				transform.localScale = Vector3.Lerp (Vector3.one, Vector3.one * scale, timer / liftTime);
				yield return null;
			}

			timer = 0f;

			while (timer < floatTime) {
				timer += Time.deltaTime;
				yield return null;
			}

			Destroy (gameObject);
		}
	}
}
