using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GBLib {
	public class PossibleSprites : MonoBehaviour {

		public List<Sprite> SpriteChoices;

		public Sprite GetRandomSprite() {
			if (SpriteChoices == null || SpriteChoices.Count == 0) {
				Debug.LogError("PossibleSprites has no possible Sprites! Using empty sprite.");
				return null;
			} 
			int index = Random.Range(0, SpriteChoices.Count);
			return SpriteChoices[index];
		}
	}
}
