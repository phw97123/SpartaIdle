using UnityEngine;

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
        LayerMask monsterLayerMask = 6;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(stateMachine.Player.transform.position, size, 3, monsterLayerMask);
        foreach (Collider2D co in colliders)
            Debug.Log(co);
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
        if(stateMachine.Target.IsDead) return false;

        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Player.transform.position).sqrMagnitude;
        return playerDistanceSqr <= 0.7 * 0.7;
    }
}
