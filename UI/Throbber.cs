using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GBLib {
	public class Throbber : MonoBehaviour {

		public Vector3 ThrobShape = Vector3.one;
		public float ThrobMag = .1f;
		public float ThrobSpeed = 5;

		private Vector3 BaseScale;

		public bool On = false;

		// Use this for initialization
		void Start() {
			BaseScale = this.transform.localScale;
		}

		public bool Toggle() {
			if (On) {
				this.transform.localScale = BaseScale;
				On = false;
			} else {
				On = true;
			}
			return On;
		}

		public void Set(bool on) {
			if (on) {
				if(On) {
					return;
				} else {
					On = true;
				}
			} else {
				if (!On) {
					return;
				} else {
					this.transform.localScale = BaseScale;
				}
			}
		}

		// Update is called once per frame
		void Update() {
			if (On) { 
				Vector3 scale = new Vector3();
				Vector3 throb = (Mathf.Sin(Time.time * ThrobSpeed) * ThrobMag + 1) * ThrobShape;
				scale.x = throb.x * BaseScale.x;
				scale.y = throb.y * BaseScale.y;
				scale.z = throb.z * BaseScale.z;
				this.transform.localScale = scale;
			}
		}
	}
}