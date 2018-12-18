using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GBLib {
	public class PossibleColors : MonoBehaviour {

		public List<Color> ColorChoices;

		public Color GetRandomColor() {
			if (ColorChoices == null || ColorChoices.Count == 0) {
				Debug.LogError("PossibleColors has no possible colors! Using White.");
				return Color.white;
			}
			int index = Random.Range(0, ColorChoices.Count);
			return ColorChoices[index];
		}
	}
}