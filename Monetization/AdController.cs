using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdController : MonoBehaviour {

	public string GameIdIOS;
	public string GameIdAndroid;
	public bool TestMode;

	public bool ShowingAd {  get { return Advertisement.isShowing; } }

	private System.Action SuccessCallback;
	private System.Action FailureCallback;

	// Use this for initialization
	void Start () {
		Debug.Log("Initializing ad controller...");

		if (Advertisement.isSupported) {
			Debug.Log("Ads ARE supported.");
#if UNITY_IOS
			Debug.Log("Initializing ads for iOS...");
			Advertisement.Initialize(GameIdIOS, TestMode);
#elif UNITY_ANDROID
			Debug.Log("Initializing ads for android...");
			Advertisement.Initialize(GameIdAndroid, TestMode);
#endif
		} else {
			Debug.Log("Ads NOT supported");
		}
		Debug.Log("Ads initialized? " + Advertisement.isInitialized);

	}

	public bool ShowAd(System.Action successCallback, string placementId=null, System.Action failCallback = null) {
		if (ShowingAd) {
			return false;
		}
		SuccessCallback = successCallback;
		FailureCallback = failCallback;
		ShowOptions opts = new ShowOptions();
		opts.resultCallback = HandleResult;
		if (placementId != null) {
			Advertisement.Show(placementId, opts);
		} else {
			Advertisement.Show(opts);
		}
		return true;
	}

	public bool AdReady(string placementId = null) {
		return Advertisement.IsReady(placementId);
	}

	private void HandleResult(ShowResult result) {
		if (result == ShowResult.Finished) {
			if (SuccessCallback != null) {
				SuccessCallback();
			}
		} else {
			if (FailureCallback != null) {
				FailureCallback();
			}
		}

		SuccessCallback = null;
		FailureCallback = null;
	}

	
}
