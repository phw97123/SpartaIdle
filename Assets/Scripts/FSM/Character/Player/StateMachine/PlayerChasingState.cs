public class PlayerChasingState : PlayerBaseState
{
    public PlayerChasingState(PlayerStateMachine palyerStateMachine) : base(palyerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }

    public override void Update()
    {
        //Move();
        //base.Update();
        //if(stateMachine.Target != null)
        //{
        //    if (IsInAttackRange())
        //        stateMachine.ChangeState(stateMachine.AttackState);
        //}
        //else
        //    stateMachine.ChangeState(stateMachine.IdleState);

        Move();
        base.Update();
        if (stateMachine.Target != null)
        {
            if (IsInAttackRange())
                stateMachine.ChangeState(stateMachine.AttackState);
        }
        else
            stateMachine.ChangeState(stateMachine.IdleState);
    }
}
