using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;

    private static int maxScore = 0;
    public static int score = 0;
    public static bool gameStarted = false;

    [Header("������ ������")]
    public GameObject startButton; // ������ UI, ��������, ������ ��� �������

    [Header("UI ��������")]
    public GameObject leaderboardButton; // ������ ����������

    private void Start()
    {
        score = 0;
        gameStarted = false;
        scoreText.text = "0";
        maxScoreText.text = maxScore.ToString(); // ���������� ������ ������

        leaderboardButton.SetActive(true);
    }

    private void Update()
    {
        scoreText.text = score.ToString();

        if (score > maxScore)
        {
            maxScore = score;
            maxScoreText.text = maxScore.ToString();
        }
    }

    // ����� ���������� �� ������� �� ������
    public void StartGame()
    {
        gameStarted = true;

        leaderboardButton.SetActive(false);

        if (startButton != null)
            Destroy(startButton);
    }
}
