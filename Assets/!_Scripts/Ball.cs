using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer ballSpriteRenderer;

    [Header("Скорость мяча")]
    [SerializeField] private float baseSpeed = 1.1f;
    [SerializeField] private float speedMultiplier = 0.1f;

    private int currentScore = 0;

    [SerializeField] private GameObject _ball;
    [SerializeField] private TextMeshProUGUI scoreText;


    private void Start()
    {
        GameManager.gameStarted = false; // ✅ Сброс перед началом

        rb = GetComponent<Rigidbody2D>();
        ballSpriteRenderer = _ball.GetComponent<SpriteRenderer>();

        if (ballSpriteRenderer != null)
        {
            ballSpriteRenderer.color = new Color(1, 1, 1, 0);
        }

        if (scoreText != null)
        {
            scoreText.alpha = 0f;
        }

        StartCoroutine(WaitForGameStart());
    }

    private void Update()
    {
        if (!GameManager.gameStarted) return;

        if (GameManager.score != currentScore)
        {
            currentScore = GameManager.score;
            UpdateBallSpeed();
        }
    }

    private void UpdateBallSpeed()
    {
        float newSpeed = baseSpeed + speedMultiplier * currentScore;
        rb.linearVelocity = rb.linearVelocity.normalized * newSpeed;
    }

    private IEnumerator WaitForGameStart()
    {
        yield return new WaitUntil(() => GameManager.gameStarted);

        yield return new WaitForSeconds(0.5f);

        ballSpriteRenderer.color = new Color(1, 1, 1, 1);

        // Показать текст
        if (scoreText != null)
        {
            scoreText.alpha = 1f;
        }


        int randomRotation = Random.Range(0, 360);
        transform.rotation = Quaternion.Euler(0, 0, randomRotation);

        rb.linearVelocity = transform.up * baseSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            GameManager.score++;

            Vector2 ballPos = transform.position;
            Vector2 paddlePos = collision.transform.position;

            float offset = (ballPos.x - paddlePos.x) / collision.collider.bounds.size.x;
            Vector2 baseDirection = new Vector2(offset, 1).normalized;

            Vector2 directionToCenter = (-ballPos).normalized;

            float centerInfluence = Random.Range(0f, 1f); // Рандом от 0 до 1
            Vector2 blendedDirection = Vector2.Lerp(baseDirection, directionToCenter, centerInfluence).normalized;

            float speed = rb.linearVelocity.magnitude;
            rb.linearVelocity = blendedDirection * speed;

            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BoundaryZone"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
