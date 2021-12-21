using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private static string highscore= "highscore";
    private static int score;
    public static int lastHighscore;

    [SerializeField] Text scoreText;
    [SerializeField] Text highscoreText;
    [SerializeField] Text liveScoreText;

    public static Score instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        score = 0;
        lastHighscore = PlayerPrefs.GetInt(highscore);
    }
    public static int TotalScore
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }
    public void ChangeScore()
    {
        score++;
        liveScoreText.text = score.ToString();
        if (score > lastHighscore)
            PlayerPrefs.SetInt(highscore, score);
    }
    public void ShowScore()
    {
        scoreText.text = "Your Score " + score;
        highscoreText.text="Highscore "+ PlayerPrefs.GetInt(highscore);
    }
}
