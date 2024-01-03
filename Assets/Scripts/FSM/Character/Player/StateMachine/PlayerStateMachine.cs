using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }
    public Health Target;
    public PlayerIdleState IdleState { get; }
    public PlayerChasingState ChasingState { get; }
    public PlayerAttackState AttackState { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; } = 5f; 

    public PlayerStateMachine(Player player)
    {
        Player = player;
        Target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();

        IdleState = new PlayerIdleState(this); 
        ChasingState = new PlayerChasingState(this);
        AttackState = new PlayerAttackState(this);
    }
}
