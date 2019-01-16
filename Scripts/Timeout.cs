using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GBLib {
	public class Timeout : MonoBehaviour {

		private Coroutine WaitRoutine;

		public bool Waiting { get { return WaitRoutine != null; } }

		public void StartTimer(float time, System.Action callback) {
			WaitRoutine = StartCoroutine(Wait(time, callback));
		}

		public void Cancel() {
			StopAllCoroutines();
			WaitRoutine = null;
		}

		private IEnumerator Wait(float time, System.Action callback) {
			yield return new WaitForSeconds(time);
			if (callback != null) {
				callback();
			}
			WaitRoutine = null;
		}

		private static IEnumerator WaitStatic(float time, System.Action callback) {
			yield return new WaitForSeconds(time);
			if (callback != null) {
				callback();
			}
		}

		public static void WaitThen(MonoBehaviour behave, float time, System.Action callback) {
			behave.StartCoroutine(WaitStatic(time, callback));
		}
	}
}