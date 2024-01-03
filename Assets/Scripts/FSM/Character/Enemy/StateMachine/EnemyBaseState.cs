using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
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

    protected void ForceMove()
    {
        stateMachine.Enemy.CharacterRigidbody2D.velocity = stateMachine.Enemy.CharacterRigidbody2D.position + stateMachine.Enemy.ForceReceiver.Movement * Time.deltaTime;
    }
    private void Move(Vector2 direction)
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.Enemy.CharacterRigidbody2D.MovePosition(stateMachine.Enemy.CharacterRigidbody2D.position +  (direction*movementSpeed) *Time.deltaTime );
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
        // TODO : 캐릭터 스텟 만들어지면 정확한 속도 넣기 
        float movementSpeed = stateMachine.MovementSpeed;
        return movementSpeed;
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

    protected bool IsInAttackRange()
    {
        if (stateMachine.Target.IsDead) return false; 

        float enemyDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;
        return enemyDistanceSqr <= 1.5 * 1.5;
    }
}
