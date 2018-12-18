using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GBLib { 
	public class InputLogicRef : InputLogic {
		public InputLogic OtherInputLogic;

		public override void OnPoke() {
			OtherInputLogic.OnPoke();
		}

		public override void OnPokeAlt() {
			OtherInputLogic.OnPokeAlt();
		}
	}
}