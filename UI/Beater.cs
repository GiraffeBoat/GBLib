using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Beater : MonoBehaviour {

	public Transform TargetObject;

	[SerializeField]
	private float BeatTime = 0.5f;
	[SerializeField]
	private Vector3 BeatSize = new Vector3(1.25f, 1.25f, 1.25f);

	private Vector3 BaseScale;
	private Coroutine BeatRoutine;

	// Use this for initialization
	void Start () {
		if (TargetObject == null) {
			TargetObject = this.gameObject.transform;
		}
		BaseScale = TargetObject.localScale;
	}

	public void BeatOnce() {
		if (BeatRoutine != null) {
			StopCoroutine(BeatRoutine);
		}
		BeatRoutine = StartCoroutine(Beat());
	}
	private IEnumerator Beat() {

		Vector3 beatScale = Vector3.Scale(BaseScale, BeatSize);
		TargetObject.localScale = beatScale;
		float timer = 0f;
		Vector3 scale;
		while (timer < BeatTime) {
			timer += Time.deltaTime;
			scale = Vector3.Lerp(beatScale, BaseScale, timer / BeatTime);
			TargetObject.localScale = scale;
			yield return null;
		}
		TargetObject.localScale = BaseScale;
		BeatRoutine = null;
	}
}
