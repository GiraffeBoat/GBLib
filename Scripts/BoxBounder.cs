using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBounder : MonoBehaviour {

	public Transform FollowTarget;
	public Transform Follower;

	public BoxBounds OuterBounds;
	public BoxBounds InnerBounds; //Must be a child of Follower object, or else behavior is undefined

	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = Follower.transform.position;
		newPosition.x = FollowTarget.position.x;
		newPosition.y = FollowTarget.position.y;
		Follower.transform.position = newPosition;

		if (InnerBounds.Left < OuterBounds.Left) {
			Follower.transform.position += Vector3.right * (OuterBounds.Left - InnerBounds.Left);
		}
		if (InnerBounds.Right > OuterBounds.Right) {
			Follower.transform.position += Vector3.right * (OuterBounds.Right - InnerBounds.Right);
		}
		if (InnerBounds.Top > OuterBounds.Top) {
			Follower.transform.position += Vector3.up * (OuterBounds.Top - InnerBounds.Top);
		}
		if (InnerBounds.Bottom < OuterBounds.Bottom) {
			Follower.transform.position += Vector3.up * (OuterBounds.Bottom - InnerBounds.Bottom);
		}
	}
}
