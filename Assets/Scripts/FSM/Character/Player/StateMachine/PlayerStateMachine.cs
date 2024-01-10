using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }
    public Health Target { get; set; }
    public PlayerIdleState IdleState { get; }
    public PlayerChasingState ChasingState { get; }
    public PlayerAttackState AttackState { get; }

    public float MovementSpeed { get; private set; }

    public PlayerStateMachine(Player player)
    {
        Player = player;
        Target = null;

        IdleState = new PlayerIdleState(this); 
        ChasingState = new PlayerChasingState(this);
        AttackState = new PlayerAttackState(this);

        MovementSpeed = player.Data.BaseSpeed;
    }

    public void InitEventTarget()
    {

    }
}
