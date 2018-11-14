using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OrthoCameraBounds : BoxBounds {

	private Camera myCamera;

	public override float Left {  get { return myCamera.ScreenToWorldPoint(Vector3.zero).x; } }
	public override float Top { get { return myCamera.ScreenToWorldPoint((Vector3.up) * myCamera.pixelHeight).y; } }
	public override float Bottom { get { return myCamera.ScreenToWorldPoint(Vector3.zero).y; } }
	public override float Right { get { return myCamera.ScreenToWorldPoint((Vector3.right) * myCamera.pixelWidth).x; } }

	void Awake() {
		myCamera = gameObject.GetComponent<Camera>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
