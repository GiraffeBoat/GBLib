using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Mostly copied from http://gamedesigntheory.blogspot.com/2010/09/controlling-aspect-ratio-in-unity.html
public class AspectRatioControl : MonoBehaviour {

	public enum AdjustType { Either, WidthOnly, HeightOnly };

	public float Width = 10f;
	public float Height = 16f;

	public AdjustType Mode = AdjustType.Either;


	// Use this for initialization
	void Start () 
	{
		// set the desired aspect ratio (the values in this example are
		// hard-coded for 16:9, but you could make them into public
		// variables instead so you can set them at design time)
		float targetaspect = Width / Height;

		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;

		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;

		// obtain camera component so we can modify its viewport
		Camera camera = GetComponent<Camera>();

		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f)
		{  
			Debug.Log("adjusting height");
			Rect rect = camera.rect;

			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;

			camera.rect = rect;
		}
		else // add pillarbox
		{
			Debug.Log("adjusting width");
			float scalewidth = 1.0f / scaleheight;

			Rect rect = camera.rect;

			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;

			camera.rect = rect;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
