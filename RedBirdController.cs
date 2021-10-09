using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBirdController : MonoBehaviour
{
    private Rigidbody2D birdRb;
    private int yMax = 20;
    //private int xLim = 200;
    public float bumpForce = 10;
    private GameObject player;
    public GameManager gameManager;
    public float maxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        birdRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Bump in a direction
        Bump();



        //rotate sprite to face direction of velocity
        Rotate();

        //Destroy if oob
        if (transform.position.y < -yMax | transform.position.y > yMax)
        {
            Destroy(gameObject);
        }
    }

    private void Bump()
    {
        int roll = Random.Range(0, 100);

        //80% chance to do nothing, 5% chance of bump left, right, up or towards player
        if (roll >= 0 & roll < 80)
        {

        }
        else if (roll >= 80 & roll < 85 & birdRb.velocity.x < maxSpeed & birdRb.velocity.y < maxSpeed)
        {
            birdRb.AddForce(Vector2.up * bumpForce * Time.deltaTime, ForceMode2D.Impulse);
        }
        else if (roll >= 85 & roll < 90 & birdRb.velocity.x < maxSpeed & birdRb.velocity.y < maxSpeed)
        {
            birdRb.AddForce(Vector2.right * bumpForce * Time.deltaTime, ForceMode2D.Impulse);
        }
        else if (roll >= 90 & roll < 95 & birdRb.velocity.x < maxSpeed & birdRb.velocity.y < maxSpeed)
        {
            birdRb.AddForce(Vector2.right * -bumpForce * Time.deltaTime, ForceMode2D.Impulse);
        }
        else if (roll >= 95 & roll < 100 & birdRb.velocity.x < maxSpeed & birdRb.velocity.y < maxSpeed)
        {
            Vector2 towardsPlayer = (player.transform.position - transform.position);
            birdRb.AddForce(towardsPlayer * bumpForce * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    private void Rotate()
    {
        if (birdRb.velocity.x > 0.3 & gameObject.transform.rotation.y != 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (birdRb.velocity.x < 0.3 & gameObject.transform.rotation.y == 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
