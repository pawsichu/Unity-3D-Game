using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float originalSpeed = 4f;
    public float stopDistance = 1f;

    private Transform target;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = originalSpeed;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        

        if (Vector3.Distance(transform.position, target.position) < stopDistance)
        {
            speed = 0f; // Stop moving when close enough to the player
         
        }

        else         
        {
            speed = originalSpeed; // Resume moving if the player moves away
        }
    }
}