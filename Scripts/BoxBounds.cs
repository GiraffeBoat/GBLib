using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBounds : MonoBehaviour {

	public float Width { get; private set; } 
	public float Height { get; private set; }

	public virtual float Top { get { return UpperLeft.position.y; } }
	public virtual float Left { get { return UpperLeft.position.x; } }
	public virtual float Right { get { return LowerRight.position.x; } }
	public virtual float Bottom { get { return LowerRight.position.y; } }

	public Transform UpperLeft;
	public Transform LowerRight;

	public Color GizmoColor = Color.yellow;
	public bool ShowGizmoAlways = false;

	// Use this for initialization
	void Start () {
		Width = LowerRight.position.x - UpperLeft.position.x;
		Height = UpperLeft.position.y - LowerRight.position.y;
	}

	private void OnDrawGizmos() {
		if (!ShowGizmoAlways) {
			return;
		}
		Vector3 ul = new Vector3(Left, Top, 0); //
		Vector3 ur = new Vector3(Right, Top, 0);
		Vector3 lr = new Vector3(Right, Bottom, 0);
		Vector3 ll = new Vector3(Left, Bottom, 0);
		/*Vector3 ul = UpperLeft.position;
		Vector3 ur = UpperLeft.position;
		ur.x = LowerRight.position.x;
		Vector3 ll = UpperLeft.position;
		ll.y = LowerRight.position.y;
		Vector3 lr = LowerRight.position;*/
		Gizmos.color = GizmoColor;
		Gizmos.DrawLine(ul, ur);
		Gizmos.DrawLine(ur, lr);
		Gizmos.DrawLine(lr, ll);
		Gizmos.DrawLine(ll, ul);
	}

	private void OnDrawGizmosSelected() {
		if (ShowGizmoAlways) {
			return;
		}
		Vector3 ul = new Vector3(Left, Top, 0); //
		Vector3 ur = new Vector3(Right, Top, 0);
		Vector3 lr = new Vector3(Right, Bottom, 0);
		Vector3 ll = new Vector3(Left, Bottom, 0);
		/*Vector3 ul = UpperLeft.position;
		Vector3 ur = UpperLeft.position;
		ur.x = LowerRight.position.x;
		Vector3 ll = UpperLeft.position;
		ll.y = LowerRight.position.y;
		Vector3 lr = LowerRight.position;*/
		Gizmos.color = GizmoColor;
		Gizmos.DrawLine(ul, ur);
		Gizmos.DrawLine(ur, lr);
		Gizmos.DrawLine(lr, ll);
		Gizmos.DrawLine(ll, ul);
	}
}
