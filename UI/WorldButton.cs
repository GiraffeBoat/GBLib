using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GBLib {
	public class WorldButton : MonoBehaviour {
		// HOW TO USE:
		// This should be on a Worldbutton prefab
		// Depends on a BoxCollider2D on the gameobject
		// Uses a rectangular clickable area that can be placed in the world and used as a simple button
		// Unknown behavior if the screen is looking at the area un-orthographically head on
		// Requires a InputLogic - derived component to accept and actually do shtuff


		public bool Enabled = true;
		Collider2D Area;

		private InputLogic Logic;

		// Use this for initialization
		void Start() {
			Area = this.GetComponentInChildren<BoxCollider2D>();
			if (Area == null) {
				Enabled = false;
				Debug.LogError("WorldButton has lost its BoxCollider2D, and can never be activated.");
			}
			Logic = this.GetComponent<InputLogic>();
		}

		// Update is called once per frame
		void Update() {
			if (!Enabled) {
				return;
			}

			if (Input.touchCount > 0) {
				Touch t = Input.GetTouch(0);
				if (CheckPoint(t.position)) {
					Click();
				}
			}

			if (Input.GetMouseButtonDown(0)) {
				if (CheckPoint(Input.mousePosition)) {
					//Mouse 1 Click
					Click();
				}
			}

			if (Input.GetMouseButtonDown(1)) {
				if (CheckPoint(Input.mousePosition)) {
					//Mouse 2 Click
					RightClick();
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

		void Click() {
			Logic.OnPoke();
		}

		void RightClick() {
			//Probably do nothing but whatever
			Logic.OnPokeAlt();
		}
	}
}
