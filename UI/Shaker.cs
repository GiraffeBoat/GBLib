using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour {

	public Transform TargetObject;

	[SerializeField]
	private float ShakeTime;
	[SerializeField]
	private Vector3 ShakeSize;

	private Vector3 BasePosition;
	private Coroutine ShakeRoutine;


	// Use this for initialization
	void Start () {
		if (TargetObject == null) {
			TargetObject = this.gameObject.transform;
		}
		BasePosition = TargetObject.position;
	}

	public void ShakeOnce() {
		if (ShakeRoutine != null) {
			StopCoroutine(ShakeRoutine);
		}
		ShakeRoutine = StartCoroutine(Shake());
	}

	private IEnumerator Shake() {
		Vector3 offset = Vector3.Scale(ShakeSize, RandomVector());
		TargetObject.position = BasePosition + offset;
		yield return new WaitForSeconds(ShakeTime);
		TargetObject.position = BasePosition;
		ShakeRoutine = null;
	}


	private Vector3 RandomVector() {
		float x = Random.value * 2 - 1;
		float y = Random.value * 2 - 1;
		float z = Random.value * 2 - 1;
		return new Vector3(x, y, z);
	}
}
