using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GBLib {
	public class LabeledProgressBar : MonoBehaviour {

		public TextAnchor Alignment;
		public string TextLabel;
		public float TextValue = 100;
		public float ValueMax = 100;

		public bool ShowLabel = true;
		public bool ShowValue = true;
		public bool ShowMax = false;

		public Font TextFont;
		public Color BackTextColor = Color.white;
		public Color FrontTextColor = Color.black;
		public RectMask2D FrontTextMask;

		public Color FilledBarColor = Color.red;
		public Color BarDropColor = Color.yellow;
		//public Color BarGrowColor; //Growth not yet supported
		public Color BGColor = Color.black;

		public float BarDropSpeed = 20f; // % per second


		[SerializeField]
		private Text BackText;
		[SerializeField]
		private Text FrontText;
		[SerializeField]
		private Image FilledBar;
		[SerializeField]
		private Image ChangeBar;
		[SerializeField]
		private Image BarBG;

		[SerializeField]
		private bool DebugInput = false;

		
		private float OldValue;
		private Coroutine runningRoutine;

		// Use this for initialization
		void Start () {
			BackText.color = BackTextColor;
			BackText.font = TextFont;
			BackText.alignment = Alignment;
			FrontText.color = FrontTextColor;
			FrontText.font = TextFont;
			FrontText.alignment = Alignment;

			BarBG.color = BGColor;
			FilledBar.color = FilledBarColor;
			ChangeBar.color = BarDropColor;
			OldValue = ValueMax;
			UpdateText ();
		}
		
		// Update is called once per frame
		void Update () {
			if (DebugInput) {
				if (Input.GetKeyDown (KeyCode.Alpha0)) {
					ChangeValue (0);
				} else if (Input.GetKeyDown (KeyCode.Alpha9)) {
					ChangeValue (.9f*ValueMax);
				} else if (Input.GetKeyDown (KeyCode.Alpha8)) {
					ChangeValue (.8f*ValueMax);
				} else if (Input.GetKeyDown (KeyCode.Alpha7)) {
					ChangeValue (.7f*ValueMax);
				} else if (Input.GetKeyDown (KeyCode.Alpha6)) {
					ChangeValue (.6f*ValueMax);
				} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
					ChangeValue (.5f*ValueMax);
				} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
					ChangeValue (.4f*ValueMax);
				} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
					ChangeValue (.3f*ValueMax);
				} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
					ChangeValue (.2f*ValueMax);
				} else if (Input.GetKeyDown (KeyCode.Alpha1)) {
					ChangeValue (.1f*ValueMax);
				} else if (Input.GetKeyDown (KeyCode.R)) {
					Reset (ValueMax);
				}
			}
				
		}

		public void ChangeValue(float newValue) {
			
			if (runningRoutine == null) {
				OldValue = Mathf.Max (TextValue, newValue);
				//Debug.Log ("NO RUNNING ROUTINE");
			} else if (newValue > OldValue) {
				StopAllCoroutines ();
				runningRoutine = null;
				OldValue = newValue;
			}//else {
				//Debug.Log ("RUNNING ROUTINE");
			//}

			TextValue = newValue;


			Vector3 changeBarScale = ChangeBar.transform.localScale;
			changeBarScale.x = OldValue / ValueMax;
			ChangeBar.transform.localScale = changeBarScale;

			Vector3 fillBarScale = FilledBar.transform.localScale;
			fillBarScale.x = TextValue / ValueMax;
			FilledBar.transform.localScale = fillBarScale;

			FrontTextMask.transform.localScale = changeBarScale;
			Vector3 frontTextScale = FrontText.transform.localScale;
			frontTextScale.x = ValueMax / OldValue;
			FrontText.transform.localScale = frontTextScale;


			if (runningRoutine == null) {
				runningRoutine = StartCoroutine (ChangeCoroutine ());
			}

		}

		public IEnumerator ChangeCoroutine() {
			while (OldValue > TextValue) {
				
				OldValue -= (ValueMax / 100) * BarDropSpeed * Time.deltaTime;

				Vector3 changeBarScale = ChangeBar.transform.localScale;
				changeBarScale.x = OldValue / ValueMax;
				ChangeBar.transform.localScale = changeBarScale;

				FrontTextMask.transform.localScale = changeBarScale;
				Vector3 frontTextScale = FrontText.transform.localScale;
				frontTextScale.x = ValueMax / OldValue;
				FrontText.transform.localScale = frontTextScale;

				UpdateText ();
				yield return null;
			}
			SetText (TextValue);
			runningRoutine = null;
		}

		private void UpdateText() {
			SetText (OldValue);
		}

		private void SetText(float theValue) {
			string text = "";
			if (ShowLabel) {
				text += TextLabel;
			}

			if (ShowValue) {
				text += Mathf.RoundToInt (theValue);
			}

			if (ShowMax) {
				text += " / " + ValueMax;
			}
				
			BackText.text = text;
			FrontText.text = text;

		}

		public void Reset(float maxValue) {
			ValueMax = maxValue;
			TextValue = maxValue;
			ChangeValue (maxValue);
		}
	}
}
