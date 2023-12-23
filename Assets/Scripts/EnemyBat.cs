using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{
    public float speed;
    public float startWaitTime;
    private float waitTime;

    public Transform movePosition;
    public Transform leftDownPosition;
    public Transform rightUpPosition;
    private new void Start()
    {
        base.Start();
        waitTime = startWaitTime;
        movePosition.position = GetRandomPosition();
    }

    private new void Update()
    {
        base.Update();
        transform.position = Vector2.MoveTowards(transform.position, movePosition.position, speed*Time.deltaTime);
        if(Vector2.Distance(transform.position,movePosition.position)<0.1f)
        {
            if (waitTime < 0)
            {
                movePosition.position = GetRandomPosition();
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    Vector2 GetRandomPosition()
    {
        Vector2 randonPosition = new Vector2(Random.Range(leftDownPosition.position.x, rightUpPosition.position.x), Random.Range(leftDownPosition.position.y, rightUpPosition.position.y));
        return randonPosition;
    }

}
