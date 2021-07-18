using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInterfaceScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> livesLeftList;
    [SerializeField] private GameObject gameOverObj;
    [SerializeField] private TMP_Text scoreText, gameOverText;

    private Transform livesLeftParent;

    private PlayerScript playerScript;


    private int livesLeft;
    private int score;

    private void Awake()
    {
        livesLeft = 3;

        playerScript = Transform.FindObjectOfType<PlayerScript>();

        EnemyScript.TakeScoreFromEnemy += AddScore;
        AsteroidController.AsteroidDeathByPlayer_Event += AddScore;
        PlayerScript.decrementLive_Event += TakeLive;
    }

    private void AddScore(int value)
    {
        score += value;
        scoreText.text = "score: " + score;
    }

    private void TakeLive(int value)
    {
        livesLeft -= value;
        
        if (livesLeft < 1)
        {
            Time.timeScale = 0f;
            gameOverText.text = $"Game over! you scored {score} points!";
            playerScript.enabled = false;
            gameOverObj.SetActive(true);
        }

        Destroy(livesLeftList[livesLeft].gameObject);

    }
}
