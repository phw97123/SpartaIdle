using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Target = null; 

    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        //base.Update();
        stateMachine.Enemy.CharacterRigidbody2D.velocity = Vector3.zero;
    }
}
