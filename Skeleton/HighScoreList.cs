using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GBLib { 
	public class HighScoreList : MonoBehaviour {
		public HighScores Scores;

		public float ListHeight;
		public TopScore TopScore;

		public TextMeshPro GlobalTotal;

		private List<TopScore> ScoreList;

		// Use this for initialization
		void Start () {
			//if (Scores != null) {
			//	LoadScores(Scores);
			//}
		}

		public void LoadScores(HighScores scores) {
			Scores = scores;
			ScoreList = new List<TopScore>();
			TopScore.Set(1, Scores.GetScore(0), Scores.LastScoreRank == 0);
			ScoreList.Add(TopScore);
			float ySpacing = ListHeight / Scores.ListSize;
			Vector3 position = TopScore.transform.position;
			for (int i = 1; i < Scores.ListSize; i++) {
				position.y -= ySpacing;
				TopScore nextScore = Instantiate<TopScore>(TopScore, position, Quaternion.identity, this.transform);
				bool blink = Scores.LastScoreRank == i;
				nextScore.Set(i + 1, Scores.GetScore(i), blink);
				ScoreList.Add(nextScore);
			}
			if (GlobalTotal != null) { 
				GlobalTotal.text = Scores.LifetimeTotal.ToString("N0");
			}
		}
	}
}