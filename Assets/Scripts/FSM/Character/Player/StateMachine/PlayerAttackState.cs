using System;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    bool aleadyCollider; 

    public PlayerAttackState(PlayerStateMachine palyerStateMachine) : base(palyerStateMachine)
    {
    }

    public override void Enter()
    {
        aleadyCollider = false;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
        StartAnimation(stateMachine.Player.AnimationData.BaseAttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
        StopAnimation(stateMachine.Player.AnimationData.BaseAttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float time = GetNormalizedTime(stateMachine.Player.Animator, "Attack");

        if (time < 1f)
        {
            if (!aleadyCollider)
            {
                stateMachine.Player.weapon.SetAttack(100);
                aleadyCollider = true;
            }
        }
        else
        {
            if (!IsInAttackRange())
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
            else if (!stateMachine.Target) 
            {
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }
            return; 
        }

    }
}
