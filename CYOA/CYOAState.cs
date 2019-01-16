using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBLib;
using GBLib.CYOA;

namespace GBLib.CYOA {
	public class CYOAState : MonoBehaviour {

		public string ShortName;
		[TextArea]
		public string Description;

		public List<CYOAOption> Options;

		public void OnDrawGizmos() {
			Gizmos.color = Color.yellow;
			foreach (CYOAOption opt in Options) {
				Gizmos.DrawLine(transform.position, opt.transform.position);
			}
		}
	}
}
