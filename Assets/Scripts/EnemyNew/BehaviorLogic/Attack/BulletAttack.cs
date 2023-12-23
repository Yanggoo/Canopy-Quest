using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

[CreateAssetMenu(fileName = "BulletAttack", menuName = "Enemy Logic/Attack Logic")]
public class BulletAttack : EnemyAttackSOBase
{
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    float interval = 1f;
    [SerializeField]
    float speed = 3;
    private Coroutine coroutine;
    public override void DoAnimationTriggerEventLogic(EnemyNew.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        //enemy.animator.SetTrigger("Attack");
        enemy.rigidBody.velocity = Vector2.zero;
        coroutine=enemy.StartCoroutine(Shoot());

    }

    IEnumerator  Shoot()
    {
        while (true)
        {
            var gb = Instantiate(bullet, enemy.transform.position, Quaternion.identity);
            gb.GetComponent<Rigidbody2D>().velocity = (playerTransform.position+new Vector3(0,1,0) - enemy.transform.position).normalized * speed;
            yield return new WaitForSeconds(interval);
        }
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        enemy.StopCoroutine(coroutine);
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        if (!enemy.isWithAttackRange)
        {
            enemy.stateMachine.ChangeState(enemy.chaseState);
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(GameObject gameObject, EnemyNew enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void RestValues()
    {
        base.RestValues();
    }


}
