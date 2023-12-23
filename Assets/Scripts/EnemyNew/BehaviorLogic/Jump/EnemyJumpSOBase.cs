using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpSOBase : ScriptableObject
{
    protected EnemyNew enemy;
    protected Transform transform;
    protected GameObject gameObject;
    protected Transform playerTransform;
    public virtual void Initialize(GameObject gameObject, EnemyNew enemy)
    {
        this.gameObject = gameObject;
        this.enemy = enemy;
        playerTransform = GameObject.Find("Player").transform;
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { RestValues(); }
    public virtual void DoFrameUpdateLogic()
    {
    }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(EnemyNew.AnimationTriggerType triggerType) { }
    public virtual void RestValues() { }
}
