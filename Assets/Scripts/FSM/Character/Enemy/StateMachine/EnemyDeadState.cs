using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("죽었닥 ");
        stateMachine.Target = null;
         Player.instance.playerData.UpdateExp(20);
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
