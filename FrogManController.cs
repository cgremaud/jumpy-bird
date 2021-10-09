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
        Patrol();
        Run();
        //Rotate(); //Causes uncontrolled flipping every frame. 
    }

    private void Patrol()
    {
        transform.Translate(Vector3.right * useSpeed * Time.deltaTime);
        if (transform.position.x - origX > radius)
        {
            //useSpeed = -speed;
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            //Rotate();
        }
        else if (transform.position.x - origX < -radius)
        {
            //useSpeed = speed;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            //Rotate();
        }
    }

    private void Run()
    {
        if (frogRb.velocity.x > 0)
        {
            frogAnim.SetBool("running_b", true);
        }
    }

    private void Rotate()
    {
        //if (frogRb.velocity.x > 0 & gameObject.transform.rotation.y != 0)
        //{
        //    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        //}
        //else if (frogRb.velocity.x < 0 & gameObject.transform.rotation.y == 0)
        //{
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        //}
    }
}
