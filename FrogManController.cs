using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogManController : MonoBehaviour
{
    public float speed = 10;
    public float useSpeed;
    public float origX;

    // Start is called before the first frame update
    void Start()
    {
        origX = transform.position.x;
        useSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * useSpeed * Time.deltaTime);
        if (transform.position.x - origX > 2 )
        {
            useSpeed = -speed;
        } else if (transform.position.x - origX < -2)
        {
            useSpeed = speed;
        }

        
    }
}
