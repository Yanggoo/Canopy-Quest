using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlyIdle", menuName = "Enemy Logic/Idle Logic")]
public class FlyIdle : EnemyIdleSOBase
{
    private Vector3 targetPosition;
    public float flyRange = 5f;
    public override void DoAnimationTriggerEventLogic(EnemyNew.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        //enemy.animator.SetBool("Idle", true);
        targetPosition = enemy.transform.position;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        //enemy.animator.SetBool("Idle", false);
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        if (enemy.isWithChaseRange)
        {
            enemy.stateMachine.ChangeState(enemy.chaseState);
            return;
        }
        if (Vector3.Distance(targetPosition, enemy.transform.position) < 0.1f|| enemy.rigidBody.velocity.magnitude<1f)
        {
            targetPosition = new Vector3(enemy.transform.position.x + RandomManager.Instance.GetRandomFloat(-flyRange, flyRange), enemy.transform.position.y + RandomManager.Instance.GetRandomFloat(-flyRange, flyRange), 0);
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
        var rb = enemy.rigidBody;
        rb.velocity = new Vector2((targetPosition - enemy.transform.position).normalized.x, (targetPosition - enemy.transform.position).normalized.y)*enemy.idleSpeed;
        
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
