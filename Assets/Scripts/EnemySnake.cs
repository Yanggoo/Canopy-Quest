using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnake : Enemy
{

    public float speed;
    public float waitTime;
    public Transform[] movePosition;

    private int i = 0;
    private bool movingRight = true;
    private float wait;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        wait = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        transform.position = Vector2.MoveTowards(transform.position, movePosition[i].position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, movePosition[i].position) < 0.1f)
        {
            if (wait > 0)
            {
                wait -= Time.deltaTime;
            }
            else
            {
                movingRight = !movingRight;
                if (movingRight)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                }
                wait = waitTime;
                i = (i + 1) % movePosition.Length;
            }
        }

    }
}
