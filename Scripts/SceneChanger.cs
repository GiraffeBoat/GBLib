using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GBLib {
	public class SceneChanger : MonoBehaviour {

		private static Stack<string> PreviousScenes = new Stack<string>();
		public static string CurrentScene { get; private set; }
		public string ThisSceneDefault;

		public void GoToScene(string sceneName) {
			if (CurrentScene == null || CurrentScene.Equals("")) {
				CurrentScene = ThisSceneDefault;
			}
			PreviousScenes.Push(CurrentScene);
			CurrentScene = sceneName;
			SceneManager.LoadScene(sceneName);
		}

		public void SilentlyGoToScene(string sceneName) {
			CurrentScene = sceneName;
			SceneManager.LoadScene(sceneName);
		}

		public void ResetToScene(string sceneName) {
			ResetStack();
			CurrentScene = sceneName;
			SilentlyGoToScene(sceneName);
		}

		public void TryGoToPreviousScene() {
			if (PreviousScenes.Count > 0) {
				string sceneName = PreviousScenes.Pop();
				SilentlyGoToScene(sceneName);
			}
		}

		public void ResetStack() {
			PreviousScenes = new Stack<string>();
		}
	}
}
