using UnityEngine;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    public float searchRadius = 10f; // 적을 찾을 반경

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
        //if (stateMachine.Target != null)
        //{
        //    if (stateMachine.Target.IsDead)
        //    {
        //        stateMachine.Target = GetClosestEnemy();
        //    }

        //}
        //else
        //{
        //    if (GetClosestEnemy() == null)
        //        stateMachine.ChangeState(stateMachine.IdleState)
        //    else
        //        stateMachine.Target = GetClosestEnemy();
        //}

        //if (stateMachine.Target == null || stateMachine.Target.IsDead || !IsInAttackRange())
        //{
        //    stateMachine.Target = GetClosestEnemy();
        //}

        //if (stateMachine.Target != null)
        //{

        //    if (stateMachine.Target.IsDead)
        //    {
        //        stateMachine.Player.playerData.UpdateExp(20);
        //    }
        //}
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
        if (stateMachine.Target == null) return;
        if (stateMachine.Target.IsDead) return;

        stateMachine.Player.CharacterRigidbody2D.velocity = Vector2.zero;
        Vector2 movementDirection = GetMovementDirection();
        float movementSpeed = GetMovementSpeed();
        float targetDistance = Vector2.Distance(stateMachine.Target.transform.position, stateMachine.Player.transform.position);

        float teleportDistance = 3f;
        float teleportSpeed = 5f;

        if (teleportDistance > targetDistance)
            stateMachine.Player.CharacterRigidbody2D.velocity = movementDirection * movementSpeed * teleportSpeed;
        else
            stateMachine.Player.CharacterRigidbody2D.velocity = movementDirection * movementSpeed;

        Rotate(movementDirection);
    }

    public void Rotate(Vector2 direction)
    {
        float angle = direction.x;
        angle = (angle <= 0) ? 0 : 180;
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        stateMachine.Player.transform.rotation = rotation;
    }

    private Vector3 GetMovementDirection()
    {
        return (stateMachine.Target.transform.position - stateMachine.Player.transform.position).normalized;
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

        return stateMachine.Player.IsAttackRange;
    }

    public Health GetClosestEnemy()
    {
        Vector3 currentPosition = stateMachine.Player.transform.position;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(currentPosition, searchRadius);

        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                Health enemyHealth = hitCollider.GetComponent<Health>();

                if (enemyHealth != null && !enemyHealth.IsDead)
                {
                    float distance = Vector2.Distance(currentPosition, hitCollider.transform.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = hitCollider.gameObject;
                    }
                }
            }
        }

        if (closestEnemy == null) return null;

        Vector2 direction = (closestEnemy.transform.position - currentPosition).normalized;
        Rotate(direction); // Rotate 함수를 필요에 따라 구현
        return closestEnemy.GetComponent<Health>();
    }
}
