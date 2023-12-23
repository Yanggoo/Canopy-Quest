using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GroundIdle", menuName = "Enemy Logic/Idle Logic")]
public class GroundIdle : EnemyIdleSOBase
{
    public override void DoAnimationTriggerEventLogic(EnemyNew.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemy.animator.SetBool("Idle", true);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        enemy.animator.SetBool("Idle", false);
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        if (enemy.isWithChaseRange)
        {
            enemy.stateMachine.ChangeState(enemy.chaseState);
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
        var rb = enemy.rigidBody;

        if (enemy.KneeDectObstacle())
        {
            rb.velocity = new Vector2(-enemy.idleSpeed * rb.transform.TransformDirection(Vector3.right).x, rb.velocity.y);
            enemy.Flip();
        }
        else
        {
            rb.velocity = new Vector2(enemy.idleSpeed * rb.transform.TransformDirection(Vector3.right).x, rb.velocity.y);
            enemy.Flip();
        }
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
