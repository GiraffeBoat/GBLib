using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowserOpener : MonoBehaviour {

	public string pageToOpen = "https://www.duckduckgo.com";

	// check readme file to find out how to change title, colors etc.
	public void OnButtonClicked() {
		InAppBrowser.DisplayOptions options = new InAppBrowser.DisplayOptions();
		options.hidesTopBar = true;
		options.displayURLAsPageTitle = false;
		options.pageTitle = "More Games from Giraffe Boat";

		InAppBrowser.OpenURL(pageToOpen, options);
	}

	public void OnClearCacheClicked() {
		InAppBrowser.ClearCache();
	}
}