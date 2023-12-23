using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public float waitTime;
    private float waitTimeRemain;
    public Transform[] movingPositions;
    private int index;
    private Transform playerDefTransform;
    // Start is called before the first frame update
    void Start()
    {
        index= 1;
        playerDefTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movingPositions[index].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, movingPositions[index].position) < 0.1f)
        {
            if (waitTimeRemain < 0.0f)
            {
                index = (index + 1) % movingPositions.Length;
                waitTimeRemain = waitTime;
            }
            else
            {
                waitTimeRemain -= Time.deltaTime;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            collision.gameObject.transform.parent = gameObject.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            collision.gameObject.transform.parent = playerDefTransform;
        }
    }
}
