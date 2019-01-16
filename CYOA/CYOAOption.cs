using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBLib;
using GBLib.CYOA;

namespace GBLib.CYOA {

	public class CYOAOption : MonoBehaviour {

		public enum OptionForceResult { DontForce, ForceSuccess, ForceFail }

		[TextArea]
		public string Description;

		public float SuccessChance = 1f;

		public CYOAState PassState;
		[TextArea]
		public string PassDescription;

		public CYOAState FailState;
		[TextArea]
		public string FailDescription;

		public void OnDrawGizmos() {
			if (PassState != null) {
				Gizmos.color = Color.green;
				Gizmos.DrawLine(transform.position, PassState.transform.position);
			}
			if (FailState != null) {
				Gizmos.color = Color.red;
				Gizmos.DrawLine(transform.position, FailState.transform.position);
			}
		}
		public CYOAState GetNextState(OptionForceResult force = OptionForceResult.DontForce) {
			if (FailState == null && PassState == null) {
				//Fatal Error, No valid states
				string msg = "Fatal error: CYOA option with no followup states.";
				Debug.LogError(msg);
				throw new System.Exception(msg);
			}

			bool success = (Random.value <= SuccessChance);
			if (SuccessChance < 1 && FailState == null) {
				//Error, chance of failure but no failure state
				Debug.LogError("Error, chance of failure but no failure state. Assuming success.");
				success = true;
			}
			if (SuccessChance > 0 && PassState == null) {
				//Error, chance of success but no success state
				Debug.LogError("Error, chance of success but no success state. Assuming failure.");
				success = false;
			}
			
			if (force == OptionForceResult.ForceFail && FailState == null) {
				//Error, forcing failure when failure is impossible
				Debug.LogError("rror, forcing failure when failure is impossible. Assuming success instead.");
				success = true;
			}
			if (force == OptionForceResult.ForceSuccess && PassState == null) {
				//Error, forcing success when success is impossible;
				Debug.LogError("Error, forcing success when success is impossible. Assuming failure instead.");
				success = false;
			}
			if (success) {
				return PassState;
			} else {
				return FailState;
			}
			

		}
	}
}