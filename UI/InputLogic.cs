using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GBLib {

	public class InputLogic : MonoBehaviour {
		// HOW TO USE:
		// Inherit from this class and implement required functions
		// So that any abstract object can respond to any sort of input from any source
		// And influence any other objects in whatever way it wants
		// without infecting either side with mish mash

		public virtual void OnPoke() { }
		public virtual void OnPokeAlt() { }
	}
}
