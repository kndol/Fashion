using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using Fashion;

[Serializable]
public class PlayerDatum
{
	public string name;
	public int score;
}

[Serializable]
public class RankingData
{
	public List<PlayerDatum> scoreData = new List<PlayerDatum>();
}

public class Ranking_UI : MonoBehaviour
{
	[SerializeField]
	Text txtNames = null;
	[SerializeField]
	Text txtScores = null;

	PlayerDatum player;
	RankingData rankingData;
	string _filePath;

	// Start is called before the first frame update
	void Start()
    {
		_filePath = Path.Combine(Application.persistentDataPath, "ranking.dat");
		player = new PlayerDatum();
		rankingData = new RankingData();

		Load();
		ShowRanking();
	}

	void Load()
	{
		try
		{
			using (Stream s = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.None))
			{
				IFormatter bf = new BinaryFormatter();
				rankingData = (RankingData)bf.Deserialize(s);
			}
		}
		catch (Exception e)
		{
			Debug.Log(e.Message);
		}
	}

	void Save()
	{
		try
		{
			using (Stream s = new FileStream(_filePath, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				IFormatter bf = new BinaryFormatter();
				bf.Serialize(s, rankingData);
			}
		}
		catch (Exception e)
		{
			Debug.Log(e.Message);
		}
	}

	public int GetHighScore() { return rankingData.scoreData.Count > 0 ? rankingData.scoreData[0].score : 0; }

	void ShowRanking()
	{
		int i = 1;
		PlayerDatum player = new PlayerDatum();
		bool updateRanking = !string.IsNullOrEmpty(Data.UserName)
			&& Data.Score > -1
			&& (rankingData.scoreData.Count < 10 || Data.Score > rankingData.scoreData[9].score);

		print(rankingData.scoreData);
		if (updateRanking)
		{
			player.name = Data.UserName;
			player.score = Data.Score;
			rankingData.scoreData.Add(player);
			rankingData.scoreData.Sort(delegate (PlayerDatum a, PlayerDatum b)
			{
				if (a.score > b.score) return -1;
				else if (a.score < b.score) return 1;
				else return 0;
			});
			if (rankingData.scoreData.Count > 10)
				rankingData.scoreData.RemoveAt(10);
			Save();
		}
		txtNames.text = "";
		txtScores.text = "";
		foreach (PlayerDatum scoreDatum in rankingData.scoreData)
		{
			bool isCurUser = updateRanking && scoreDatum == player;
			string curOpen = isCurUser ? "<color=yellow>" : "";
			string curClose = isCurUser ? "</color>" : "";
			txtNames.text += curOpen + scoreDatum.name + curClose + "\n";
			txtScores.text += curOpen + scoreDatum.score.ToString() + curClose + "\n";
			i++;
		}
		Data.Score = -1;
	}

}
