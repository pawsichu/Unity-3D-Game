using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform target;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        Debug.Log("enemy position" + transform.position + " | target position" + target.position);

    }
}