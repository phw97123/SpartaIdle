using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
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

        if (stateMachine.Target != null)
        {
            if (stateMachine.Target.IsDead)
            {
                stateMachine.Target = GetClosestEnemy();
            }
        }
        else
            stateMachine.ChangeState(stateMachine.IdleState);

        if (IsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
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
        if(stateMachine.Target == null) { return; }

        Vector2 movementDirection = GetMovementDirection();
        Rotate(movementDirection);
        Move(movementDirection);
    }

    private void Move(Vector2 direction)
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.Player.CharacterRigidbody2D.MovePosition(stateMachine.Player.CharacterRigidbody2D.position 
            + (direction * movementSpeed) * Time.deltaTime);
    }

    public void Rotate(Vector2 direction)
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
        if (!stateMachine.Target) return false; 
        if (stateMachine.Target.IsDead) return false;

        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Player.transform.position).sqrMagnitude;
        return playerDistanceSqr <= 0.8f * 0.8f;
    }

    public Health GetClosestEnemy()
    {
        Vector2 currentPosition = stateMachine.Player.transform.position;
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");

        if (targets.Length == 0)
        {
            return null;
        }

        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject target in targets)
        {
            float distance = Vector2.Distance(currentPosition, target.transform.position);

            if (distance <= closestDistance)
            {
                closestDistance = distance;
                closestEnemy = target;
            }
        }
        return closestEnemy.GetComponent<Health>();
    }
}
