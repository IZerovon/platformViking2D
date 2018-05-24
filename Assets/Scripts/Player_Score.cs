using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Score : MonoBehaviour {

    public static Player_Score playerScoreInstance;
    private float timeLeft = 180;
    public static int playerScore = 0;
    public GameObject timeLeftUI;
    public GameObject playerScoreUI;

    void Update ()
    {
        PlayerVariables playerVariables = GetComponent<PlayerVariables>();

        timeLeft -= Time.deltaTime;
        timeLeftUI.gameObject.GetComponent<Text>().text = ("" + (int)timeLeft);
        playerScoreUI.gameObject.GetComponent<Text>().text = ("" + playerScore);
        if (timeLeft < 0.1f)
        {
            playerVariables.Respawn();
            timeLeft = 180;
            playerScore = 0;
        }
	}

    public void CountScore()
    {
        playerScore = playerScore + (int)(timeLeft * 10);
        if (playerScore > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", playerScore);
        }
        
    }
}