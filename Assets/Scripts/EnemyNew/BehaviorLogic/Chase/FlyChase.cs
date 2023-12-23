using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FlyChase", menuName = "Enemy Logic/Chase Logic")]
public class FlyChase : EnemyChaseSOBase
{
    public override void DoAnimationTriggerEventLogic(EnemyNew.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        //enemy.animator.SetBool("Chase", true);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        //enemy.animator.SetBool("Chase", false);
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        if (!enemy.attackWithCollide&& enemy.isWithAttackRange)
        {
            enemy.stateMachine.ChangeState(enemy.attackState);
        }
        else if (!enemy.isWithChaseRange)
        {
            enemy.stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
        var rb = enemy.rigidBody;
        rb.velocity = new Vector2((playerTransform.position - enemy.transform.position).normalized.x, (playerTransform.position - enemy.transform.position).normalized.y) * enemy.chaseSpeed;
        //enemy.Flip();
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
