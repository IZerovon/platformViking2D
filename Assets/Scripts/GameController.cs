using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController gameControllerInstance;
    public Text coinText;
    public Slider healthSlider;
    public float playerHealth;

    private Quaternion originalCameraRotation;

    [HideInInspector]
    public int coins;

	void Start ()
    {
        gameControllerInstance = this;
        coins = 0;
        originalCameraRotation = Camera.main.transform.rotation;
	}
	
	void Update ()
    {
        coinText.text = "" + coins;
        healthSlider.value = playerHealth;
	}

    public void ScreenShake()
    {
        Camera.main.DOShakeRotation(0.2f, 6, 40, 90);

        Invoke("ResetCameraRotation", 0.2f);
    }

    private void ResetCameraRotation()
    {
        Camera.main.transform.rotation = originalCameraRotation;
    }

    public void LoadLevel(int levelToLoad)
    {
        int previousCoins = PlayerPrefs.GetInt("coins");
        previousCoins += coins;
        PlayerPrefs.SetInt("coins", coins);
        SceneManager.LoadScene(levelToLoad);
    }
}
