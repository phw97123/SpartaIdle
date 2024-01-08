using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine enemyBaseState) : base(enemyBaseState)
    {
    }

    public override void Enter()
    {
        stateMachine.Enemy.CharacterRigidbody2D.velocity = Vector3.zero;
        stateMachine.Enemy.weapon.SetAttack(1, 0, 0); 
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (!IsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
        }
        else if (stateMachine.Target == null)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}
