using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine palyerStateMachine) : base(palyerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Player.weapon.SetAttack(stateMachine.Player.Data.Damage);
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.Target.IsDead)
        {
            stateMachine.Target = GetClosestEnemy();
            return;
        }

        if (!IsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
        }
        else if(!stateMachine.Target)
            stateMachine.ChangeState(stateMachine.IdleState);
    }
}
