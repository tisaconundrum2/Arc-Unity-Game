using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScore : MonoBehaviour {

    Text scoreTextUI;
    int score;
    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
            this.score = value;
            UpdatScoreTextUI();
        }
    }
	// Use this for initialization
	void Start () {
        scoreTextUI = GetComponent<Text>();
	}

    void UpdatScoreTextUI()
    {
        string scoreStr = string.Format("{0:000000000}", score);
        scoreTextUI.text = scoreStr;
    }
}
