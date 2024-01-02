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
        Move(); 
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.animator.SetBool(animationHash, false);
    }

    private void Move()
    {
        Vector2 movementDirection = GetMovementDirection();

        Move(movementDirection);
    }

    private void Move(Vector2 direction) 
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.Player.characterController.Move((direction * movementSpeed) * Time.deltaTime); 
    }

    private Vector2 GetMovementDirection()
    {
        return (stateMachine.Target.transform.position - stateMachine.Player.transform.position).normalized;
    }

    private float GetMovementSpeed()
    {
        // TODO : ĳ���� ���� ��������� ��Ȯ�� �ӵ� �ֱ� 
        float movementSpeed = stateMachine.MovementSpeed;
        return movementSpeed; 
    }
}
