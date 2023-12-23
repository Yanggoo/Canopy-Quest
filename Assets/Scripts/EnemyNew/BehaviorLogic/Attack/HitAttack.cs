using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HitAttack", menuName = "Enemy Logic/Attack Logic")]
public class HitAttack : EnemyAttackSOBase
{
    public override void DoAnimationTriggerEventLogic(EnemyNew.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemy.animator.SetTrigger("Attack");
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        if (enemy.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Attack")
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
