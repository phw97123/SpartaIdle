using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerBaseState : IState
{

    protected PlayerStateMachine stateMachine;
    Vector2 size = new Vector2(1, 1);

    public PlayerBaseState(PlayerStateMachine palyerStateMachine)
    {
        stateMachine = palyerStateMachine;
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
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
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
        stateMachine.Player.CharacterRigidbody2D.MovePosition(stateMachine.Player.CharacterRigidbody2D.position + (direction * movementSpeed) * Time.deltaTime);

    }

    private void Rotate(Vector2 direction)
    {
        float angle = direction.x;
        angle = (angle < 0) ? 0 : 180;
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        stateMachine.Player.transform.rotation = rotation;
    }

    private Vector2 GetMovementDirection()
    {
        return (stateMachine.Target.transform.position - stateMachine.Player.transform.position).normalized;
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

        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Player.transform.position).sqrMagnitude;
        return playerDistanceSqr <= .7f * .7f;
    }

    protected void ForceMove()
    {
        stateMachine.Player.CharacterRigidbody2D.MovePosition(stateMachine.Player.CharacterRigidbody2D.position + stateMachine.Player.ForceReceiver.Movement * Time.deltaTime);
    }

    public Transform GetClosestEnemy()
    {
        Vector2 currentPosition = stateMachine.Player.transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(currentPosition, 10f, 6);

        if (colliders.Length == 0)
        {
            return null;
        }

        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D collider in colliders)
        {
            float distance = Vector2.Distance(currentPosition, collider.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = collider.transform;
            }
        }

        return closestEnemy;
    }
}
