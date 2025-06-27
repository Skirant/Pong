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

    [Header("������ ������")]
    public GameObject startButton;

    [Header("������� �������")]
    public GameObject LBExample;
    public LeaderboardYG leaderboardYG;
    public GameObject LiderbordButtun;

    private void Start()
    {
        LBExample.SetActive(false);
        LiderbordButtun.SetActive(true);
        scoreText.gameObject.SetActive(false);

        score = 0;
        gameStarted = false;
        scoreText.text = "0";

        // ��������� �������� �� ����������
        maxScoreText.text = YG2.saves.maxScore.ToString();
    }

    private void Update()
    {
        scoreText.text = score.ToString();

        if (score > YG2.saves.maxScore)
        {
            YG2.saves.maxScore = score;
            maxScoreText.text = score.ToString();

            // ��������� ���������� ������
            YG2.SaveProgress();

            // ���������� ������ � ��������� (����� ��� ����������� ��������)
            YG2.SetLeaderboard(leaderboardYG.nameLB, score);
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
        LBExample.SetActive(!LBExample.activeSelf);

        FindAnyObjectByType<AudioManager>().Play("Button");
    }
}
