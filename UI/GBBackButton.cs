using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBLib;

[RequireComponent(typeof(GBButton))]
public class GBBackButton : MonoBehaviour {
	private GBButton MyButton;

	void Awake() {
		MyButton = GetComponent<GBButton>();
	}
	
	public void AlsoClick() {
		if (MyButton.Enabled) {
			MyButton.Click();
		}
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			AlsoClick();
		}
	}
}
