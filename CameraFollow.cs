
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    private Transform playerTransform;
    public Vector3 offset;
    public float smoothSpeed = 0.001f; //higher = faster
    private void Start()
    {
        Vector3 offset = new Vector3(0, 0, -10);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        Vector3 temp = transform.position;

        temp.x = playerTransform.position.x;

        transform.position = temp;


    }
}
