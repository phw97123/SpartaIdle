using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private bool alreadyAppliedForce;
    private bool alreadyAppliedDealing;

    public PlayerAttackState(PlayerStateMachine palyerStateMachine) : base(palyerStateMachine)
    {
    }

    public override void Enter()
    {
        alreadyAppliedForce = false; 
        alreadyAppliedDealing = false;
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

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            stateMachine.Player.weapon.GetComponent<Collider2D>().enabled = alreadyAppliedDealing;

            if (!alreadyAppliedDealing && normalizedTime >= stateMachine.Player.Data.Dealing_Start_TransitionTime)
            {
                stateMachine.Player.weapon.GetComponent<Collider2D>().enabled = alreadyAppliedDealing;
                stateMachine.Player.weapon.SetAttack(stateMachine.Player.Data.Damage);
                alreadyAppliedDealing = true;
            }
        }
        else
        {
            if (!IsInAttackRange())
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }
        }
    }
}
