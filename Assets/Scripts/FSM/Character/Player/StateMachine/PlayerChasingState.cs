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
        base.Update();
    }
}
