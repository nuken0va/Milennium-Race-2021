using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreScript : MonoBehaviour
{
    public string prefix = "";
    GameObject player;
    Text text;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        text = GetComponent<Text>();
        score = 0;
        text.text = prefix + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        var curScore = (int)((player.transform.position.x + 30) / 60);
        if (score < curScore)
        {
            score = curScore;
            text.text = prefix + score.ToString();
        }
    }
}
