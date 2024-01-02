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
        // TODO : 캐릭터 스텟 만들어지면 정확한 속도 넣기 
        float movementSpeed = stateMachine.MovementSpeed;
        return movementSpeed; 
    }
}
