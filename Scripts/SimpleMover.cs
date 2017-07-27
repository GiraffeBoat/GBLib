using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMover : MonoBehaviour {

	public float Speed;
	public Vector3 Direction;
	public bool AutoNormalize = true;

	void Start() {
		if (AutoNormalize) {
			Direction.Normalize ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Direction * Speed * Time.deltaTime;
	}
}
