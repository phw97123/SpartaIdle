using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine palyerStateMachine) : base(palyerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash); 
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash); 
    }

    public override void Update()
    {
        base.Update();

        if(stateMachine.Target)
        {
            stateMachine.ChangeState(stateMachine.ChasingState); 
        }
        else if(IsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.AttackState);
        }
    }
}
