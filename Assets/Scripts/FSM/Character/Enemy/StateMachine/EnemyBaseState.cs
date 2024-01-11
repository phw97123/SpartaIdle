using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        stateMachine.Enemy.GetComponent<Health>().OnDie += OnDead; 
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void Update()
    {
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, false);
    }

    public void Move()
    {
        Vector2 movementDirection = GetMovementDirection();
        Rotate(movementDirection);
        Move(movementDirection);
    }

    private void Move(Vector2 direction)
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.Enemy.CharacterRigidbody2D.velocity = direction * movementSpeed;
    }

    private void Rotate(Vector2 direction)
    {
        float angle = direction.x;
        angle = (angle < 0) ? 0 : 180;
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        stateMachine.Enemy.transform.rotation = rotation;
    }

    private Vector2 GetMovementDirection()
    {
        return (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).normalized;
    }

    private float GetMovementSpeed()
    {
        // TODO : ĳ���� ���� ��������� ��Ȯ�� �ӵ� �ֱ� 
        float movementSpeed = stateMachine.MovementSpeed;
        return movementSpeed;
    }

    protected bool IsInAttackRange()
    {
        if (stateMachine.Target.IsDead) return false;

        float enemyDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;
        return enemyDistanceSqr <= 0.45f * 0.45f;
    }

    private void OnDead()
    {
        stateMachine.ChangeState(stateMachine.DeadState); 
    }
}
