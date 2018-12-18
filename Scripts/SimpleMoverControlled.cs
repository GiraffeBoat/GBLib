using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoverControlled : MonoBehaviour {

	public float Speed;

	public bool ArrowKeys;
	public bool WASD;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( (Input.GetKey(KeyCode.UpArrow) && ArrowKeys) ||
			(Input.GetKey(KeyCode.W) && WASD) ) {
			transform.position += Vector3.up * Speed * Time.deltaTime;
		}
		if ((Input.GetKey(KeyCode.LeftArrow) && ArrowKeys) ||
			(Input.GetKey(KeyCode.A) && WASD)) {
			transform.position += Vector3.left * Speed * Time.deltaTime;
		}
		if ((Input.GetKey(KeyCode.DownArrow) && ArrowKeys) ||
			(Input.GetKey(KeyCode.S) && WASD)) {
			transform.position += Vector3.down * Speed * Time.deltaTime;
		}
		if ((Input.GetKey(KeyCode.RightArrow) && ArrowKeys) ||
			(Input.GetKey(KeyCode.D) && WASD)) {
			transform.position += Vector3.right * Speed * Time.deltaTime;
		}
	}
}
