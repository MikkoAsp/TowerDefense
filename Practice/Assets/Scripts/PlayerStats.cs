using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static float money;
    public static float lives;
    public float startLives = 15;
    public float startMoney = 400;

    public static bool gameOver;
    public static bool inMainMenu;

    [Header("User Interface")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI livesText;
    public GameObject gameOverUI;

    [Header("Debug purposes only")]
    [SerializeField] bool isInMainMenu;
    [SerializeField] bool isGameOver;
    private void Start()
    {
        gameOver = false;
        inMainMenu = false;
        money = startMoney;
        lives = startLives;
    }
    private void Update()
    {
        if (Input.GetKeyDown("1") && Time.timeScale < 5f)
        {
            Time.timeScale += 1;
            Debug.Log(Time.timeScale);
        }
        if(Input.GetKeyDown("2") && Time.timeScale > 1)
        {
            Time.timeScale -= 1;
            Debug.Log(Time.timeScale);
        }
        if (Input.GetKeyDown("f"))
        {
            money += 1000;
        }
        if (Input.GetKey("g"))
        {
            LoseLives(10);
        }
        UpdatePlayerUI();
        isInMainMenu = inMainMenu;
        isGameOver = gameOver;
    }
    void UpdatePlayerUI()
    {
        moneyText.text = "$" + money.ToString(); 
    }
    public void LoseLives(float amount)
    {
        lives -= amount;
        livesText.text = "Lives: " + Mathf.Round(lives).ToString();
        if(lives <= 0) {
            if (!gameOver)
                Die();
            else
                return;
        }
    }
    void Die()
    {
        gameOver = true;
        lives = 0;
        gameOverUI.SetActive(true);
    }
}
