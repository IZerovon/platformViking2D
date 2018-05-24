using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public Text coinText;
    public Text scoreText;
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    public void Start()
    {
        coinText.text = "Coins: " + PlayerPrefs.GetInt("coins");
        scoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
        PlayerPrefs.SetInt("TempScore", 0);
    }

    public void Update()
    {
        coinText.text = "Coins: " + PlayerPrefs.GetInt("coins");
        scoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
        PlayerPrefs.SetInt("TempScore", 0);
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetCoins()
    {
        PlayerPrefs.SetInt("coins", 0);
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("Highscore", 0);
    }
}
