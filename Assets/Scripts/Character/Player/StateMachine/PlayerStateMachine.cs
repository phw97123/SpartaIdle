using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }
    public GameObject Target { get; private set; }

    public PlayerIdleState IdleState { get; }
    public PlayerChasingState ChasingState { get; }
    public PlayerAttackState AttackState { get; }


    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; } = 1f; 

    public PlayerStateMachine(Player player)
    {
        Player = player;
        Target = GameObject.FindGameObjectWithTag("Enemy");

        IdleState = new PlayerIdleState(this); 
        ChasingState = new PlayerChasingState(this);
        AttackState = new PlayerAttackState(this);
    }
}
