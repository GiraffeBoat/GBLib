using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMover : MonoBehaviour {

	public float Speed;
	public Vector3 Direction;
	public bool AutoNormalize = true;
	public float RotationSpeed;


	void Start() {
		if (AutoNormalize) {
			Direction.Normalize ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Direction * Speed * Time.deltaTime;
		transform.Rotate(Vector3.back * RotationSpeed * Time.deltaTime);
	}
}
