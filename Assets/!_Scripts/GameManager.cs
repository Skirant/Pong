using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;

    public static int score = 0;
    public static bool gameStarted = false;

    [Header("Кнопка старта")]
    public GameObject startButton;

    [Header("Таблица лидеров")]
    public GameObject LBExample;
    public LeaderboardYG leaderboardYG;
    public GameObject LiderbordButtun;

    private float lastToggleTime = -0.5f; // последнее время переключения
    private float toggleCooldown = 0.5f;  // задержка в секундах

    private List<KeyCode> resetSequence = new List<KeyCode> { KeyCode.Alpha2, KeyCode.Alpha4, KeyCode.Alpha8, KeyCode.Alpha8 };
    private int currentResetStep = 0;

    private void Start()
    {
        LBExample.SetActive(false);
        LiderbordButtun.SetActive(true);
        scoreText.gameObject.SetActive(false);

        score = 0;
        gameStarted = false;
        scoreText.text = "0";

        // Загружаем максимум из сохранений
        maxScoreText.text = YG2.saves.maxScore.ToString();
    }

    private void Update()
    {
        scoreText.text = score.ToString();

        if (score > YG2.saves.maxScore)
        {
            YG2.saves.maxScore = score;
            maxScoreText.text = score.ToString();
            YG2.SaveProgress();
            YG2.SetLeaderboard(leaderboardYG.nameLB, score);
        }

        // Проверка ввода последовательности клавиш
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(resetSequence[currentResetStep]))
            {
                currentResetStep++;

                if (currentResetStep >= resetSequence.Count)
                {
                    ResetSaves();
                    currentResetStep = 0;
                }
            }
            else
            {
                currentResetStep = 0;
            }
        }
    }

    public void StartGame()
    {
        YG2.InterstitialAdvShow();

        FindAnyObjectByType<AudioManager>().Play("StartGame");

        scoreText.gameObject.SetActive(true);
        LiderbordButtun.SetActive(false);

        gameStarted = true;
        if (startButton != null)
            Destroy(startButton);
    }

    public void OnLBExample()
    {
        if (!LBExample.activeSelf)
        {
            // Включаем, но только если прошло 1 секунда
            if (Time.time - lastToggleTime >= toggleCooldown)
            {
                LBExample.SetActive(true);
                lastToggleTime = Time.time;
            }
        }
        else
        {
            // Выключаем всегда
            LBExample.SetActive(false);
        }

        FindAnyObjectByType<AudioManager>().Play("Button");
    }

    public void UpdateLeaderBoard()
    {
        leaderboardYG.UpdateLB();
    }

    public void ResetSaves()
    {
        YG2.SetDefaultSaves();
        YG2.SaveProgress();
        print("----------------");
    }
}
