using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;
    public bool isLevelComplete;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI healthText;
    public GameObject titleScreen;
    public Button restartButton;
    public int score;
    public int playerHealth;
    public int currentLevel;
    public SpawnManager spawnManager;
    public float spawnInterval = 2;
    public float spawnIntervalMin = 3;
    public float spawnIntervalMax = 7;
    public GameObject enemyObject;
    public GameObject player;
    public AudioSource mainAudio;
    public AudioSource deathSound;
    public AudioSource hurtSound;


    // Start is called before the first frame update
    public void StartGame()
    {
        playerHealth = 10; //hacky but checking
        isGameActive = true;
        UpdateScore(0);
        UpdateHealthText(0); //needed? 
        titleScreen.gameObject.SetActive(false);
        StartCoroutine(SpawnEnemy());
        mainAudio.gameObject.SetActive(false);
    }
    public IEnumerator SpawnEnemy()
    {
        while (isGameActive)
        {
            
            yield return new WaitForSeconds(spawnInterval);
            float x = Random.Range(player.transform.position.x - 20, player.transform.position.x + 20);
            float y = Random.Range(player.transform.position.y +3, player.transform.position.y + 10);
            Vector2 spawnPos = new Vector2(x, y);
            Instantiate(enemyObject, spawnPos, enemyObject.transform.rotation);
            spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax); //possibly refactor this to set a new random min and max every time and then draw from between them? Seems calculation heavy and slow. 
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateHealthText(int healthLost)
    {
        if (playerHealth <= 5)
        {
            healthText.color = Color.red;
        }
        playerHealth -= healthLost;
        healthText.text = "Health: " + playerHealth;
        hurtSound.Play();
    }

    public void EndLevel()
    {

    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        deathSound.Play();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
