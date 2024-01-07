using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine palyerStateMachine) : base(palyerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Player.CharacterRigidbody2D.velocity = Vector2.zero;
        stateMachine.Player.weapon.SetAttack(stateMachine.Player.Data.Damage, stateMachine.Player.Data.Force, stateMachine.Player.Data.KnckbackDuration);
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

        if (stateMachine.Target)
        {
            if (!IsInAttackRange())
            {
                stateMachine.Target = GetClosestEnemy();
                stateMachine.ChangeState(stateMachine.ChasingState);
            }
        }
        else
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}
