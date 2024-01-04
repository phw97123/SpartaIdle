using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
        Move();

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
        if (stateMachine.Target == null) { return; }

        Vector2 movementDirection = GetMovementDirection();
        float movementSpeed = GetMovementSpeed();
        float targetDistance = Vector2.Distance(stateMachine.Target.transform.position, stateMachine.Player.transform.position);

        float teleportDistance = 1f;

        if (teleportDistance > targetDistance)
        {
            stateMachine.Player.CharacterRigidbody2D.velocity = movementDirection * movementSpeed *1.5f;
        }
        else
        {
            stateMachine.Player.CharacterRigidbody2D.velocity = movementDirection * movementSpeed;
        }
        Rotate(movementDirection);
    }

    public void Rotate(Vector2 direction)
    {
        float angle = direction.x;
        angle = (angle <= 0) ? 0 : 180;
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        stateMachine.Player.transform.rotation = rotation;
    }

    private Vector2 GetMovementDirection()
    {
        return (stateMachine.Target.transform.position - stateMachine.Player.transform.position);
    }

    private float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed;
        return movementSpeed;
    }

    protected bool IsInAttackRange()
    {
        if (!stateMachine.Target) return false;
        if (stateMachine.Target.IsDead) return false;

        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Player.transform.position).sqrMagnitude;
        return playerDistanceSqr <= 1f * 1f;
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
