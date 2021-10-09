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
    public GameObject titleScreen;
    public Button restartButton;
    public int score;
    public SpawnManager spawnManager;
    public float spawnInterval = 2;
    public GameObject enemyObject;
    public GameObject player;

    // Start is called before the first frame update
    public void StartGame()
    {
        isGameActive = true;
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
        StartCoroutine(SpawnEnemy());
    }
    public IEnumerator SpawnEnemy()
    {
        while (isGameActive)
        {
            
            yield return new WaitForSeconds(spawnInterval);
            float x = Random.Range(player.transform.position.x - 10, player.transform.position.x + 10);
            float y = Random.Range(player.transform.position.y +1, player.transform.position.y + 4);
            Vector2 spawnPos = new Vector2(x, y);
            Instantiate(enemyObject, spawnPos, enemyObject.transform.rotation);
            //spawnInterval = Random.Range(0, 5);
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
