using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlatformConnect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Authenticate and register a ProcessAuthentication callback
		// This call needs to be made before we can proceed to other calls in the Social API
		Social.localUser.Authenticate (ProcessAuthentication);
	}

	// This function gets called when Authenticate completes
	// Note that if the operation is successful, Social.localUser will contain data from the server. 
	void ProcessAuthentication (bool success) {
		if (success) {
			Debug.Log ("Authenticated, checking achievements");

			// Request loaded achievements, and register a callback for processing them
			Social.LoadAchievements (ProcessLoadedAchievements);
		}
		else
			Debug.Log ("Failed to authenticate");
	}

	// This function gets called when the LoadAchievement call completes
	void ProcessLoadedAchievements (IAchievement[] achievements) {
		if (achievements.Length == 0)
			Debug.Log ("Error: no achievements found");
		else
			Debug.Log ("Got " + achievements.Length + " achievements");

		// You can also call into the functions like this
		Social.ReportProgress ("Achievement01", 100.0, result => {
			if (result)
				Debug.Log ("Successfully reported achievement progress");
			else
				Debug.Log ("Failed to report achievement");
		});
	}

	void LoadScores(IScore[] scores) {
		if (scores.Length > 0) {
			//Debug.Log("Got " + scores.Length + " scores");
			string myScores = "Leaderboard:\n";
			foreach (IScore score in scores) {
				myScores += "\t" + score.userID + " " + score.formattedValue + " " + score.date + "\n";
			}
			//Debug.Log(myScores);
		} else {
			Debug.Log("No scores loaded");
		}
	}

	void ReportScore(long score, string leaderboardID)
	{
		Debug.Log("Reporting score " + score + " on leaderboard " + leaderboardID);
		Social.ReportScore(score, leaderboardID, success => {
			Debug.Log(success ? "Reported score successfully" : "Failed to report score");
		});
	}
		
}
