using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GBLib {
	[Serializable]
	public class HighScores {

		List<int> TopScores;
		public int LifetimeCount { get; private set; }
		public int LifetimeTotal { get; private set; }

		public int ListSize;

		public int LastScoreRank;

		public bool LastIsRecord { get { return LastScoreRank != -1; } }

		private StorageSystem Storage;
		[SerializeField]
		private string KeyBase;

		private bool IsLoaded;

		public HighScores(StorageSystem storage, string keyBase, int listSize) {
			ListSize = listSize;
			KeyBase = keyBase;
			Storage = storage;
			LastScoreRank = -1;
			TopScores = new List<int>();
			ReadData();
		}

		private void AssertLoaded() {
			if (!IsLoaded) {
				throw new System.Exception("Trying to use a High Score list before it's been loaded! Bad!");
			}
		}

		public int GetScore(int index) {
			if (TopScores == null) {
				return 123;
			}
			return TopScores[index];
		}

		private void ReadData() {
			//Read Top Scores
			string key = "";
			for (int i = 0; i < ListSize; i++) {
				key = string.Format("{0}-Top-{1}", KeyBase, i);
				TopScores.Add(Storage.Get<int>(key));
			}
			//Read Lifetime Score
			key = string.Format("{0}-LifetimeTotal", KeyBase);
			LifetimeTotal = Storage.Get<int>(key);
			//Read Lifetime Count
			key = string.Format("{0}-LifetimeCount", KeyBase);
			LifetimeCount = Storage.Get<int>(key);
			IsLoaded = true;
		}

		private void WriteData() {
			AssertLoaded();
			//Write Top Scores
			string key = "";
			for (int i = 0; i < ListSize; i++) {
				key = string.Format("{0}-Top-{1}", KeyBase, i);
				Storage.Set<int>(key, TopScores[i]);
			}
			//Write Lifetime Score
			key = string.Format("{0}-LifetimeTotal", KeyBase);
			Storage.Set<int>(key, LifetimeTotal);
			//Write Lifetime Count
			key = string.Format("{0}-LifetimeCount", KeyBase);
			Storage.Set<int>(key, LifetimeCount);
		}

		public int AddScore(int newScore) {
			AssertLoaded();
			//returns index of new high score, if it is a new high score, otherwise -1
			if (newScore == 0)
				return -1;
			LifetimeCount += 1;
			LifetimeTotal += newScore;
			TopScores.Add(newScore);
			TopScores.Sort();
			TopScores.Reverse(); // descending, please
			WriteData();
			int place = GetNewIndex(newScore);
			if (place < ListSize) {
				LastScoreRank = place;
				return place;
			} else {
				LastScoreRank = -1;
				return -1;
			}
		}

		int GetNewIndex(int score) {
			for (int i = 0; i < ListSize; i++) {
				if (TopScores[i] == score) {
					return i;
				}
			}
			return 99;
		}

	}
}
