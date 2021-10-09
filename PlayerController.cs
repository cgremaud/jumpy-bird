using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D playerRb;
    public float jumpForce = 10;
    public float bumpForce = 5;
    public float gravityMod = 1;
    public bool isOnGround = true;
    private Animator playerAnim;
    public float maxSpeed = 5;
    public float speed = 1;
    private bool touchedEnemy = false;
    public float maneuverability = 2;
    public GameManager gameManager;
    public float deathThreshhold = -7.5f;
    private int comboCount = 1;
   
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityMod;
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move left/right when left/right arrow is pressed
        //MovePlayer();
        if ((playerRb.velocity.x < maxSpeed & playerRb.velocity.x > -maxSpeed) & isOnGround)
        {
            playerRb.AddForce(Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime, ForceMode2D.Impulse);
        } else /*if ((playerRb.velocity.x < maxSpeed & playerRb.velocity.x > -maxSpeed) & !isOnGround) */
        {
            playerRb.AddForce(Vector2.right * Input.GetAxis("Horizontal") * maneuverability * Time.deltaTime, ForceMode2D.Impulse);
            //push back if going greater than maxSpeed
            if (playerRb.velocity.x > maxSpeed)
            {
                Vector2 pushBack = new Vector2(5f, 0);
                playerRb.AddForce(Vector2.right * -pushBack, ForceMode2D.Force);
            } else if (playerRb.velocity.x <  -maxSpeed)
            {
                Vector2 pushBack = new Vector2(5f, 0);
                playerRb.AddForce(Vector2.right * pushBack, ForceMode2D.Force);
            }
        }
        //handle rotation and animation states.
        //RotateAnimPlayer();
        if (playerRb.velocity.x > 0)
        {
            if (gameObject.transform.rotation.y != 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            playerAnim.SetBool("running_b", true);
        } else if (playerRb.velocity.x < 0)
        {
            if (gameObject.transform.rotation.y == 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            playerAnim.SetBool("running_b", true);
        } else
        {
            playerAnim.SetBool("running_b", false);
           
        }
        //Jump
        //Jump();
        if (Input.GetKeyDown(KeyCode.Space) & (isOnGround | touchedEnemy))
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isOnGround = false;
            touchedEnemy = false;
            playerAnim.SetTrigger("jump_t");
        }

        //Check for death
        if (playerRb.position.y < deathThreshhold)
        {
            gameManager.GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            touchedEnemy = false;
            comboCount = 1;
            //Debug.Log("Touch Grass");
        } 
        if (collision.collider.GetType() == typeof(BoxCollider2D) & collision.gameObject.CompareTag("EnemyHurtbox") & playerRb.velocity.y <= 0.5)
        {
            gameManager.UpdateScore(10 * comboCount);
            if (!isOnGround & touchedEnemy)
            {
                comboCount += 1;
            }
            playerRb.AddForce(Vector2.up * bumpForce, ForceMode2D.Impulse);
            Destroy(collision.gameObject);
            Debug.Log("Collision!");
            touchedEnemy = true;
        }
        if (collision.gameObject.CompareTag("EnemyHurtbox") & collision.collider.GetType() == typeof(PolygonCollider2D) & playerRb.velocity.y <= 0.5)
        {
            
            touchedEnemy = true;
        }
    }
}