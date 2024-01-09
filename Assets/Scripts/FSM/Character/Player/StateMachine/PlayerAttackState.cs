using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine palyerStateMachine) : base(palyerStateMachine)
    {

    }

    public override void Enter()
    {
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
        stateMachine.Player.CharacterRigidbody2D.velocity = Vector2.zero;
        base.Update();

    //    if (stateMachine.Target)
    //    {
    //        if (!IsInAttackRange())
    //        {
    //            if (stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
    //            {
    //                stateMachine.Target = GetClosestEnemy();
    //                stateMachine.ChangeState(stateMachine.ChasingState);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        stateMachine.ChangeState(stateMachine.IdleState);
    //    }

        if(stateMachine.Target == null)
            stateMachine.ChangeState(stateMachine.IdleState);

        if(!IsInAttackRange())
            stateMachine.ChangeState(stateMachine.ChasingState);
    }

}
