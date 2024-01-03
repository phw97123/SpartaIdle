using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine enemyBaseState) : base(enemyBaseState)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
    }

    public override void Update()
    {
        base.Update();
       
        if (!IsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
        else if (stateMachine.Target == null)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }
}
