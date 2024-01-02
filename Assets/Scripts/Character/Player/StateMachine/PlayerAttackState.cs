public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine palyerStateMachine) : base(palyerStateMachine)
    {
    }

    public override void Enter()
    {
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

        if (!stateMachine.Target)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
        else if (!IsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
    }

}
