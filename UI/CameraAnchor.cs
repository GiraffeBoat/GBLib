using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchor : MonoBehaviour {

	public enum Anchor { UpperLeft, UpperCenter, UpperRight, MiddleLeft, MiddleCenter, MiddleRight, LowerLeft, LowerCenter, LowerRight, Upper, Middle, Lower, Left, Center, Right }

	public OrthoCameraBounds AnchorCamera;
	public Anchor BaseAnchor;
	public float OffsetX;
	public float OffsetY;

	// Use this for initialization
	void Start () {
		float baseX = transform.position.x;
		float baseY = transform.position.y;
		switch (BaseAnchor) {
			case Anchor.LowerLeft:
			case Anchor.MiddleLeft:
			case Anchor.UpperLeft:
			case Anchor.Left:
				baseX = AnchorCamera.Left;
				break;
			case Anchor.LowerCenter:
			case Anchor.MiddleCenter:
			case Anchor.UpperCenter:
			case Anchor.Center:
				baseX = (AnchorCamera.Left + AnchorCamera.Right) / 2;
				break;
			case Anchor.LowerRight:
			case Anchor.MiddleRight:
			case Anchor.UpperRight:
			case Anchor.Right:
				baseX = AnchorCamera.Right;
				break;
		}

		switch (BaseAnchor) {
			case Anchor.LowerLeft:
			case Anchor.LowerCenter:
			case Anchor.LowerRight:
			case Anchor.Lower:
				baseY = AnchorCamera.Bottom;
				break;
			case Anchor.MiddleLeft:
			case Anchor.MiddleCenter:
			case Anchor.MiddleRight:
			case Anchor.Middle:
				baseY = (AnchorCamera.Top + AnchorCamera.Bottom) / 2;
				break;
			case Anchor.UpperLeft:
			case Anchor.UpperCenter:
			case Anchor.UpperRight:
			case Anchor.Upper:
				baseY = AnchorCamera.Top;
				break;
		}
		Vector3 newPos = transform.position;
		newPos.x = baseX + OffsetX;
		newPos.y = baseY + OffsetY;
		transform.position = newPos;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
