using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public Text ScoreText;
    public int score = 0;
    public int maxScore;
    public PauseMenu victory;
    private float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        waitTime = 0.5f;
    }

    public void AddScore(int newScore)
    {
        score += newScore;
    }

    // Update is called once per frame
    public void UpdateScore()
    {
        ScoreText.text = "Objective: Defeat " + score + "/5 Bandits";
    }

    void Update()
    {
        UpdateScore();
        if (score == maxScore)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                victory.GameWon();
            }
        }
    }
}
