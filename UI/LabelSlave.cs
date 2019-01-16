using GBLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LabelSlave : MonoBehaviour {

	// HOW TO USE:
	// Implement the ILabelMaster interface on some component on an object, and reference the object here as LabelMaster
	// The field here is a GameObject because Unity does not recognize Interface fields in the editor
	// The interface implelentor shall allow aribtrary strings to be recieved and values delivered for them
	// This class automatically finds components with a text attribute (UI Text, Textmesh, or Inputfield) to use as things to change

	// Once: On Start, (and when called explicitly)
	// OnDemand: Never, except on called explicitly
	// Constant: Every frame (and when called explicitly, but that would be dumb in this case)
	// Secondly: Once a second (and, you guessed it, when called explicitly) 
	public enum UpdateSchedule { Once, OnDemand, Constant, Secondly };


	public GameObject LabelMaster;
	private ILabelMaster Master; 
	public string Variable;
	public UpdateSchedule Schedule;

	private float timer;

	private TextMesh Mesh;
	private TextMeshPro TextPro;
	private Text UIText;
	private InputField UIInput;

	private TextBlinker Blinker;
	private Throbber Throbber;

	void Start() {
		Master = LabelMaster.GetComponent<ILabelMaster>();

		Mesh = GetComponentInChildren<TextMesh>();
		TextPro = GetComponentInChildren<TextMeshPro>();
		UIText = GetComponentInChildren<Text>();
		UIInput = GetComponentInChildren<InputField>();

		Blinker = GetComponentInChildren<TextBlinker>();
		Throbber = GetComponentInChildren<Throbber>();

		if (Schedule == UpdateSchedule.Once || Schedule == UpdateSchedule.OnDemand) {
			UpdateNow();
		}
	}
	
	// Update is called once per frame
	void Update () {
		switch ( Schedule ) {
			case UpdateSchedule.Once:
			case UpdateSchedule.OnDemand:
				return;
			case UpdateSchedule.Secondly:
				timer -= Time.deltaTime;
				if (timer > 0) 
					return;
				timer = 1f;
				UpdateNow();
				return;
			case UpdateSchedule.Constant:
				UpdateNow();
				return;
		}
	}

	public void UpdateNow() {
		string printThis = string.Format("{0}", Master.GetLabelValue(Variable));
		if (Blinker) {
			if ((bool)Master.GetLabelValue(Variable + "_Blink")) {
				Blinker.Active = true;
			} else {
				Blinker.Active = false;
			}
			Blinker.ChangeText(printThis);
		}
		if (Throbber) {
			if ((bool) Master.GetLabelValue(Variable + "_Throb")) {
				Throbber.Set(true);
			} else {
				Throbber.Set(false);
			}
		}
		if (Mesh) {
			Mesh.text = printThis;
		}
		if (TextPro) {
			TextPro.text = printThis;
		}
		if (UIText) {
			UIText.text = printThis;
		}
		if (UIInput) {
			UIInput.text = printThis;
		}
	}

	public void UpdateLabel() {
		if (Master != null && Schedule == UpdateSchedule.OnDemand) {
			UpdateNow();
		}
	}
}
