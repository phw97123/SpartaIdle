public class PlayerStateMachine : StateMachine
{
    public prevPlayer Player { get; }
    public Health Target { get; set; }
    public PlayerIdleState IdleState { get; }
    public PlayerChasingState ChasingState { get; }
    public PlayerAttackState AttackState { get; }

    public float MovementSpeed { get; private set; }

    public PlayerStateMachine(prevPlayer player)
    {
        Player = player;
        Target = null;

        IdleState = new PlayerIdleState(this); 
        ChasingState = new PlayerChasingState(this);
        AttackState = new PlayerAttackState(this);

        MovementSpeed = player.Data.BaseSpeed;
    }
}
