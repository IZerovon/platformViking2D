using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {

    public int levelToLoad;
    string currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (currentScene == "Scene02" && other.CompareTag("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerVariables>().CountScore();
            PlayerPrefs.SetInt("TempScore", 0);
        }
        if (other.CompareTag("Player"))
        {
            GameController.gameControllerInstance.LoadLevel(levelToLoad);
        }
    }

}
