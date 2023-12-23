using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GroundJump", menuName = "Enemy Logic/Jump Logic")]
public class GroundJump : EnemyJumpSOBase
{
    public override void DoAnimationTriggerEventLogic(EnemyNew.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemy.animator.SetBool("Jump",true);
        var rb = enemy.rigidBody;
        rb.velocity = new Vector2(rb.velocity.x>0?enemy.idleSpeed:-enemy.idleSpeed, enemy.jumpYspeed);

    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        enemy.animator.SetBool("Jump", false);
        var rb = enemy.rigidBody;
        rb.velocity = new Vector2(rb.velocity.x > 0 ? enemy.idleSpeed : -enemy.idleSpeed, 0f);
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        if (enemy.isOnGround())
        {
            enemy.stateMachine.ChangeState(enemy.chaseState);
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
        var rb = enemy.rigidBody;
        rb.velocity = new Vector2(rb.velocity.x > 0 ? enemy.idleSpeed : -enemy.idleSpeed, rb.velocity.y);
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
