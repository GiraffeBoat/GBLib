using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelMasterBasic : MonoBehaviour, ILabelMaster {
	// HOW TO USE
	// Put LabelSlaves on objects with a text field, and they will update their value to whatever this says
	// Implement ILabelMaster on an object to 
	// Or just use this class, which is the most basic generic key-value store implementation 

	private Dictionary<string, object> AllMyValues;

	// Use this for initialization
	void Start () {
		AllMyValues = new Dictionary<string, object>();
	}

	public object GetLabelValue(string key) {
		object value = "";
		AllMyValues.TryGetValue(key, out value);
		return value;
	}

	public void SetLabelValue(string key, object value) {
		AllMyValues[key] = value;
	}
}
