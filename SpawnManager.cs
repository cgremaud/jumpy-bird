using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyObject;
    public int spawnInterval;
    public bool isActive;
    public PlayerController player;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void SpawnEnemy()
    {
        while (gameManager.isGameActive)
        {
            int x = Random.Range(-38, 39);
            int y = Random.Range(0, 4);
            Vector2 spawnPos = new Vector2(x, y);
            Instantiate(enemyObject, spawnPos, enemyObject.transform.rotation);
            spawnInterval = Random.Range(0, 5);
        }
    }
        
}
