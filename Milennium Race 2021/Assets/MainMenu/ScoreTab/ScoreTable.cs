using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTable : MonoBehaviour
{
    public Transform entryTemplate;
    // Start is called before the first frame update
    void Start()
    {
        var scoreTable = GameInfo.GetScoreTable();
        print(JsonUtility.ToJson(scoreTable));
        if (scoreTable == null) return;
        for (int i = 0; i < scoreTable.scoreList.Count && i < 3; i++)
        {
            Transform tableEnrty = CreateEntry(i+1, scoreTable.scoreList[i]);
        }
    }

    private Transform CreateEntry(int place, GameInfo.ScoreEntry scoreEntry)
    {        
        Transform entryTransform = Instantiate(entryTemplate, transform);

        entryTransform.Find("Place/Text").GetComponent<Text>().text = place.ToString();
        entryTransform.Find("Score/Text").GetComponent<Text>().text = scoreEntry.score.ToString();
        entryTransform.Find("Time/Text").GetComponent<Text>().text = scoreEntry.time.ToString("n2") + " s.";
        string car = "";
        switch (scoreEntry.carId)
        {
            case 0: car = "MK1"; break;
            case 1: car = "4x4"; break;
        }
        entryTransform.Find("Car/Text").GetComponent<Text>().text = car;

        return entryTransform;
        //entryTransform.Find("Death/Text").GetComponent<Text>().text = "Soon";
    }
}
