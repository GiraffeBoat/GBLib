using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StorageSystem  {
	//Someday I'll turn this into different subclasses for different storage methods
	//you know, this could be static, probably
	public T Get<T>(string id) {
		if (typeof(T) == typeof(int)) {
			int value = PlayerPrefs.GetInt(id, 0);
			return (T)Convert.ChangeType(value, typeof(T));
		} else if (typeof(T) == typeof(float)) {
			float value = PlayerPrefs.GetFloat(id, 0f);
			return (T)Convert.ChangeType(value, typeof(T));
		} else if (typeof(T) == typeof(string)) {
			string value = PlayerPrefs.GetString(id, "");
			return (T)Convert.ChangeType(value, typeof(T));
		} else {
			throw new Exception(string.Format("StorageSystem cannot get type: {0} (Valid types:, {1}, {2}, {3})", typeof(T), typeof(int), typeof(float), typeof(string)));
		}
		//return default(T);
	}

	public bool Set<T>(string id, T value) {
		if (typeof(T) == typeof(int)) {
			int intVal = (int)Convert.ChangeType(value, typeof(int));
			PlayerPrefs.SetInt(id, intVal);
			return true;
		} else if (typeof(T) == typeof(float)) {
			float floatVal = (float)Convert.ChangeType(value, typeof(float));
			PlayerPrefs.SetFloat(id, floatVal);
			return true;
		} else if (typeof(T) == typeof(string)) {
			string strVal = (string)Convert.ChangeType(value, typeof(string));
			PlayerPrefs.SetString(id, strVal);
			return true;
		} else {
			throw new Exception(string.Format("StorageSystem cannot et type: {0} (Valid types:, {1}, {2}, {3})", typeof(T), typeof(int), typeof(float), typeof(string)));
		}
		//return false;
	}
}
