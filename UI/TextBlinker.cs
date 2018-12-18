using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GBLib {
	/*
	 * This component is used to flash the individual letters of a text display 
	 * as a specified color different from the default of the text field
	 * It requires either a TextMesh component or a UI Text component on the same object to work
	 * Automatically reads the default text, and will always use that
	 * Spacing determines the number of non-flashed letters between each flashed letter
	 * Spacing 0 means entire text will flash
	 * Speed is the time in game seconds before the next tick
	 * Desired features:
	 * multi-letter flash spans
	 * Multiple flashing colors (not sure what the design of that would be)
	 * Optional word-based flashing, rather than character-based
	 * Choose direction of movement
	 */

	public class TextBlinker : MonoBehaviour {

		public Color ColorA;
		public float speed;
		public int spacing;

		private EitherText TextObj;

		private string currentDefaultText;
		private Color currentDefaultColor;

		private float timer = 0f;
		private int currentOffset;

		public bool Active;

		// Use this for initialization
		void Start() {
			TextObj = new EitherText(gameObject);
			UpdateDefaults();
		}

		public void UpdateDefaults() {
			currentDefaultText = TextObj.GetDefaultText();
			currentDefaultColor = TextObj.GetDefaultColor();
		}

		// Update is called once per frame
		void Update() {
			if (!Active) {
				return;
			}
			timer -= Time.deltaTime;
			if (timer <= 0f) {
				//Blink!
				currentOffset++;
				TextObj.SetText(GenerateString(currentOffset));
				timer = speed;
			}
		}

		public Color GetDefaultColor() {
			return currentDefaultColor;
		}

		public void ChangeText(string newText) {
			currentDefaultText = newText;
		}

		private string GenerateString(int offset) {
			if (spacing < 0 || !Active) {
				return currentDefaultText;
			}
			if (spacing == 0) {
				if (offset % 2 == 0) {
					string all = "<color=" + ColorToHTML(ColorA) + ">";
					all += currentDefaultText;
					all += "</color>";
					return all;
				}
				else {
					return currentDefaultText;
				}
			}

			string str = "";
			bool flashOn = false;
			int i = 0;
			foreach (char c in currentDefaultText) {
				if (c != ' ') {
					i++;
				}
				if ((i + offset) % (spacing + 1) == 0) {
					//flash on
					if (!flashOn) {
						//Not already On
						//open color tag
						str += "<color=" + ColorToHTML(ColorA) + ">";
						flashOn = true;
					}
				}
				else {
					//Flash off
					if (flashOn) {
						//Flash on
						//close color tag
						str += "</color>";
						flashOn = false;
					}
				}
				str += c;
			}
			//End closing color tag
			if (flashOn) {
				str += "</color>";
				flashOn = false;
			}
			return str;
		}

		private string ColorToHTML(Color color) {
			string hex = "#";
			int r = (int)(0xff * color.r);
			int g = (int)(0xff * color.g);
			int b = (int)(0xff * color.b);
			//Hackity shmakity do
			//the html string don't work if you give it, say 3, because it expects 03. 
			//I *should* do proper string formating stuff... buuuut
			r = Mathf.Max(r, 0x10);
			g = Mathf.Max(g, 0x10);
			b = Mathf.Max(b, 0x10);
			hex += r.ToString("X");
			hex += g.ToString("X");
			hex += b.ToString("X");
			return hex;
		}

		private class EitherText {
			TextMesh MeshText;
			Text UIText;

			string InitialText;

			public EitherText(GameObject obj) {
				TextMesh mesh = obj.GetComponent<TextMesh>();
				if (mesh != null) {
					MeshText = mesh;
					InitialText = MeshText.text;
					if (!MeshText.richText) {
						Debug.LogWarning("Mesh Text for TestBlinker has rich text disabled! This WILL look dumb!");
					}
					return;
				}
				Text text = obj.GetComponent<Text>();
				if (text != null) {
					UIText = text;
					InitialText = UIText.text;
					if (!UIText.supportRichText) {
						Debug.LogWarning("Mesh Text for TestBlinker has rich text disabled! This WILL look dumb!");
					}
					return;
				}
				Debug.LogError("Trying to initialize text blinker on an object that has neither a Text nor TextMesh component");
			}

			public void SetText(string newText) {
				if (MeshText) {
					MeshText.text = newText;
				}
				else if (UIText) {
					UIText.text = newText;
				}
			}

			public string GetDefaultText() {
				if (MeshText) {
					return MeshText.text;
				}
				else if (UIText) {
					return UIText.text;
				}
				return "";
			}
			public Color GetDefaultColor() {
				if (MeshText) {
					return MeshText.color;
				}
				else if (UIText) {
					return UIText.color;
				}
				return Color.black;
			}

			public string GetInitialText() {
				return InitialText;
			}

		}
	}
}