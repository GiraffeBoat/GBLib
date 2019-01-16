using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using TMPro;

namespace GBLib {
	public class GBButton : MonoBehaviour {
		// HOW TO USE:
		// This should be on a Worldbutton prefab
		// Depends on a BoxCollider2D on the gameobject
		// Uses a rectangular clickable area that can be placed in the world and used as a simple button
		// Unknown behavior if the screen is looking at the area un-orthographically head on


		public bool Enabled = true;
		Collider2D Area;
		SpriteRenderer AreaImage;
		TextMeshPro TextPro;
		[SerializeField]
		private Color DisabledAreaColor;
		private Color EnabledAreaColor;
		[SerializeField]
		private Color DisabledTextColor;
		private Color EnabledTextColor;

		public static bool Belay;
		public bool BelayOverride;

		public UnityEvent OnClick;

		public void SetEnabled(bool enabled) {
			Enabled = enabled;
			if (AreaImage != null) {
				AreaImage.color = Enabled ? EnabledAreaColor : DisabledAreaColor;
			}
			if (TextPro != null) {
				TextPro.color = Enabled ? EnabledTextColor : DisabledTextColor;
			}
		}

		public void SetText(string newText) {
			if (TextPro != null) {
				TextPro.text = newText;
			}
		}

		void Awake() {
			Area = this.GetComponentInChildren<BoxCollider2D>();
			TextPro = this.GetComponentInChildren<TextMeshPro>();
			if (TextPro != null) {
				EnabledTextColor = TextPro.color;
			}
			if (Area == null) {
				Enabled = false;
				Debug.LogError("WorldButton has lost its BoxCollider2D, and can never be activated.");
			} else {
				AreaImage = Area.GetComponent<SpriteRenderer>();
				if (AreaImage != null) {
					EnabledAreaColor = AreaImage.color;
				}
			}
		}

		// Use this for initialization
		void Start() {
			//Make sure colors are set correctly
			SetEnabled(Enabled);
		}

		// Update is called once per frame
		void Update() {
			if (!AllowedToClick()) {
				return;
			}

			if (Input.GetMouseButtonDown(0)) {
				if (CheckPoint(Input.mousePosition)) {
					//Mouse 1 Click
					Click();
                    return;
                }
			}

            if (Input.touchCount > 0) {
                Touch t = Input.GetTouch(0);
                if (t.phase == TouchPhase.Began && CheckPoint(t.position)) {
                    Click();
                    return;
                }
            }
        }

		private bool CheckPoint(Vector3 pixelCoords) {
			Vector3 point = Camera.main.ScreenToWorldPoint(pixelCoords);
			point.z = this.transform.position.z; // depth isn't real
			if (Area.bounds.Contains(point)) {
				return true;
			}
			else {
				return false;
			}
		}


		public void Click() {
			if (!AllowedToClick()) {
				return;
			}
			OnClick.Invoke();
		}

		public bool AllowedToClick() {
			if (!Enabled) {
				return false;
			}
			if (Belay) {
				if (BelayOverride) {
					return true;
				} else {
					return false;
				}
			} else {
				return true;
			}
		}

		public void SetOverrideAndBelay(bool on) {
			if (on) {
				BelayOverride = true;
				Belay = true;
			} else {
				BelayOverride = false;
				Belay = false;
			}
		}
	}
}
