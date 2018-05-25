using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVariables : MonoBehaviour {

    public static PlayerVariables playerVariablesInstance;
    public Transform startPosition;
    public float health = 10;
    public GameObject coinParticles;
    public AudioClip coinPickup;
    public GameObject timeLeftUI;
    public GameObject playerScoreUI;
    public static int playerScore = 0;

    private float timeLeft = 180;
    private float damageTimer;
    private AudioSource myAudioSource;

	void Start ()
    {
        health = 10;
        myAudioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("TempScore") > playerScore)
        {
            playerScore = PlayerPrefs.GetInt("TempScore");
        }
	}
	
	void Update ()
    {
        PlayerPrefs.SetInt("TempScore", playerScore);

        damageTimer += Time.deltaTime;
        GameController.gameControllerInstance.playerHealth = health;

        timeLeft -= Time.deltaTime;
        timeLeftUI.gameObject.GetComponent<Text>().text = ("" + (int)timeLeft);
        playerScoreUI.gameObject.GetComponent<Text>().text = ("" + playerScore);
        if (timeLeft < 0.1f)
        {
            Respawn();
        }
    }

    public void CountScore()
    {
        playerScore = playerScore + (int)(timeLeft * 10);
        if (playerScore > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", playerScore);
        }

    }

    public void Harm(float dmg)
    {
        if(damageTimer > 1.0f)
        {
            health -= dmg;
            damageTimer = 0;
            GameController.gameControllerInstance.ScreenShake();
        }

        if(health < 1)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        transform.position = startPosition.position;
        health = 10;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            playerScore += 50;
            other.gameObject.SetActive(false);
            Instantiate(coinParticles, other.transform.position, Quaternion.identity);
            GameController.gameControllerInstance.coins++;
            myAudioSource.pitch = Random.Range(0.5f, 1.5f);
            myAudioSource.PlayOneShot(coinPickup, 0.5f);
            
        }
    }
}
