using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class HighScores : MonoBehaviour {

	public string fileName = "playerInfo.dat";
	public float highscore_level1 = 999f; // really this is best time, not high score; if it's 0, it hasn't been set yet; TODO fix that hack
	public float highscore_level2 = 999f;

	void OnEnable() {
		Load ();
	}

	void OnDisable() {
		Save ();
	}

	public float GetHighScore(int level) {
		if (level == 1) {
			return highscore_level1;
		} else {
			return highscore_level2;
		}
	}
		
	public void SetHighScore(int level, float timeElapsed) {
		if (level == 1 && timeElapsed <= highscore_level1) {
			highscore_level1 = timeElapsed;

		} if (level == 2 && timeElapsed <= highscore_level2) {
			highscore_level2 = timeElapsed;
		}
	}

	public void Save() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/" + fileName);

		PlayerData data = new PlayerData();
		data.highscore_level1 = highscore_level1;
		data.highscore_level2 = highscore_level2;
		bf.Serialize(file, data);
		file.Close();
	}

	public void Load() {
		if (File.Exists(Application.persistentDataPath + "/" + fileName)) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			highscore_level1 = data.highscore_level1;
			highscore_level2 = data.highscore_level2;

		}
	}

}

[Serializable]
class PlayerData {
	public float highscore_level1;
	public float highscore_level2;
}