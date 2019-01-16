using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whomper : MonoBehaviour {

	public float Magnitude;
	public float WhompTime;

	private float Timer;
	private Vector3 BaseScale;
	private Vector3 TargetScale;

	public void Whomp() {
		if (Timer > 0) {
			transform.localScale = BaseScale;
		}
		Timer = WhompTime;
		BaseScale = this.gameObject.transform.localScale;
		TargetScale = BaseScale * Magnitude;
	}
	
	// Update is called once per frame
	void Update () {
		if (Timer > 0) {
			float progress = (WhompTime - Timer) / WhompTime;
			if (progress < 0.5f) {
				//Shrinking in
				float halfProgress = progress *2f;
				transform.localScale = Vector3.Lerp(BaseScale, TargetScale, halfProgress);
			} else {
				//Growing Out
				float halfProgress = progress * 2f - 1;
				transform.localScale = Vector3.Lerp(TargetScale, BaseScale, halfProgress);
			}
			Timer -= Time.deltaTime;
			if (Timer <=0 ) {
				transform.localScale = BaseScale;
			}
		}
	}
}
