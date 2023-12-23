using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GroundChase", menuName = "Enemy Logic/Chase Logic")]
public class GroundChase : EnemyChaseSOBase
{
    public override void DoAnimationTriggerEventLogic(EnemyNew.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemy.animator.SetBool("Chase", true);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        enemy.animator.SetBool("Chase", false);
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        if (!enemy.attackWithCollide&&enemy.isWithAttackRange)
        {
            enemy.stateMachine.ChangeState(enemy.attackState);
        }
        else if(!enemy.isWithChaseRange)
        {
            enemy.stateMachine.ChangeState(enemy.idleState);
        }else if (enemy.canJump&&enemy.KneeDectObstacle()&&!enemy.HeadDectObstacle())
        {
            enemy.stateMachine.ChangeState(enemy.jumpState);
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
        var rb = enemy.rigidBody;
        rb.velocity = new Vector2(enemy.chaseSpeed * ((playerTransform.position - enemy.transform.position).x > 0 ? 1 : -1), rb.velocity.y);
        enemy.Flip();
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
