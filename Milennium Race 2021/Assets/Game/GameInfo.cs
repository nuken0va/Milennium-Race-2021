using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameInfo
{
    public static int selectedCar = 0;


    public static ScoreTable GetScoreTable()
    {
        string jsonString = PlayerPrefs.GetString("scoreTable");
        return JsonUtility.FromJson<ScoreTable>(jsonString);
    }
    public static void AddScore(int score = 0, int carId = 0, float time = 0, int deathType = -1)
    {
        Debug.Log(DateTime.Now);
        ScoreEntry highscoreEntry = new ScoreEntry
        {
            score = score,
            carId = carId,
            time = time,
            date = DateTime.Now,
            deathType = deathType
        };

        string jsonString = PlayerPrefs.GetString("scoreTable");
        ScoreTable scores = JsonUtility.FromJson<ScoreTable>(jsonString);

        if (scores == null)
        {
            scores = new ScoreTable()
            {
                scoreList = new List<ScoreEntry>()
            };
        }


        // Add new entry to Highscores
        scores.scoreList.Add(highscoreEntry);
        scores.scoreList.Sort();
        if (scores.scoreList.Count > 3)
        {
            for (int i = 3; i < scores.scoreList.Count; i++)
            {
                scores.scoreList.RemoveAt(i);
            }
        }

        // Save updated Highscores
        string json = JsonUtility.ToJson(scores);
        PlayerPrefs.SetString("scoreTable", json);
        PlayerPrefs.Save();
    }

    public class ScoreTable
    {
        public List<ScoreEntry> scoreList;
    }

    [System.Serializable]
    public class ScoreEntry : IComparable
    {
        public int score = 0;
        public int carId = 0;
        public float time = 0;
        public DateTime date;
        public int deathType = 0;

        public int CompareTo(object obj)
        {
            return ((ScoreEntry)obj).score - this.score;
        }
    }
}

