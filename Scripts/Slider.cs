using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GBLib {
	public class Slider : MonoBehaviour {

		public enum SlideState { Out, GoingIn, In, GoingOut };

		public AnimationCurve TransitionCurve;
		public float TimeToSlide;

		public Transform Root;
		public Transform SlideFinish;
		private StaticTransform Origin;
		private StaticTransform Target;

		public UnityEvent OnSlideInStart;
		public UnityEvent OnSlideInFinish;
		public UnityEvent OnSlideOutStart;
		public UnityEvent OnSlideOutFinish;

		public SlideState State { get; private set; }
		public bool Showing { get { return State == SlideState.GoingIn || State == SlideState.In; } }

		private Coroutine MoveRoutine;

		// Use this for initialization
		void Awake() {
			State = SlideState.Out;
			//Root = this.transform;
			Origin = new StaticTransform(Root);
			Target = new StaticTransform(SlideFinish);
		}

		// Update is called once per frame
		void Update() {
			DEBUGINPUT();
		}

		public void SlideIn() {
			if (MoveRoutine == null) {
				OnSlideInStart.Invoke();
				MoveRoutine = StartCoroutine(Moving(false));
			}

		}

		public void SlideOut() {
			if (MoveRoutine == null) {
				OnSlideOutStart.Invoke();
				MoveRoutine = StartCoroutine(Moving(true));
			}
		}

		public void JumpIn() {
			if (MoveRoutine != null) {
				StopCoroutine(MoveRoutine);
				MoveRoutine = null;
			}
			Root.position = Target.position;
			Root.localScale = Target.scale;
			Root.rotation = Target.rotation;
			State = SlideState.In;
		}

		public void JumpOut() {
			if (MoveRoutine != null) {
				StopCoroutine(MoveRoutine);
				MoveRoutine = null;
			}
			Root.position = Origin.position;
			Root.localScale = Origin.scale;
			Root.rotation = Origin.rotation;
			State = SlideState.Out;
		}

		private IEnumerator Moving(bool reverse) {
			if (reverse) {
				State = SlideState.GoingOut;
			} else {
				State = SlideState.GoingIn;
			}
			float time = 0;
			Vector3 pos, scale;
			Quaternion rot;
			while (time < TimeToSlide) {
				time += Time.deltaTime;
				float u = time / TimeToSlide;
				if (reverse) {
					u = 1 - u;
				}
				float v = TransitionCurve.Evaluate(u);
				pos = Vector3.LerpUnclamped(Origin.position, Target.position, v);
				scale = Vector3.LerpUnclamped(Origin.scale, Target.scale, v);
				rot = Quaternion.LerpUnclamped(Origin.rotation, Target.rotation, v);

				Root.position = pos;
				Root.localScale = scale;
				Root.rotation = rot;

				yield return null;
			}
			MoveRoutine = null;
			if (reverse) {
				State = SlideState.Out;
				OnSlideOutFinish.Invoke();
			} else {
				State = SlideState.In;
				OnSlideInFinish.Invoke();
			}

		}

		void DEBUGINPUT() {
			if (Input.GetKeyDown(KeyCode.RightArrow)) {
				SlideIn();
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow)) {
				SlideOut();
			}
		}

		private class StaticTransform {
			public Vector3 position;
			public Vector3 scale;
			public Quaternion rotation;

			public StaticTransform(Transform source) {
				position = source.position;
				scale = source.localScale;
				rotation = source.rotation;
			}
		}

	}
}
