using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine palyerStateMachine) : base(palyerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Target = GetClosestEnemy();
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

        //if (stateMachine.Target && !stateMachine.Target.IsDead)
        //{
        //    if (!IsInAttackRange())
        //        stateMachine.ChangeState(stateMachine.ChasingState);
        //    else
        //    {
        //        stateMachine.ChangeState(stateMachine.AttackState);
        //    }
        //}

        if (stateMachine.Target)
        {
            stateMachine.Player.target = stateMachine.Target;
            stateMachine.ChangeState(stateMachine.ChasingState);
        }
        else
            stateMachine.Target = GetClosestEnemy();


        //if (stateMachine.Target)
        //{
        //    if (!IsInAttackRange())
        //        stateMachine.ChangeState(stateMachine.ChasingState);
        //    else
        //    {
        //        stateMachine.ChangeState(stateMachine.AttackState);
        //    }
        //}
    }
}
