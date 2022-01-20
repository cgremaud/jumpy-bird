using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogManController : MonoBehaviour
{
    public float speed = 10;
    public float useSpeed;
    public float origX;
    public int scoreValue = 10;
    public float radius = 2;
    private Rigidbody2D frogRb;
    private Animator frogAnim;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        frogAnim = GetComponent<Animator>();
        frogRb = GetComponent<Rigidbody2D>();
        origX = transform.position.x;
        useSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            Patrol();
            Run();
        }
        
        
    }

    private void Patrol()
    {
        transform.Translate(Vector3.right * useSpeed * Time.deltaTime);
        if (transform.position.x - origX > radius)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x - origX < -radius)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Run()
    {
        if (frogRb.velocity.x > 0)
        {
            frogAnim.SetBool("running_b", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
