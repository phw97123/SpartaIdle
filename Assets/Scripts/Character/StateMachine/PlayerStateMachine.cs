using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public Player Player { get; }
    public GameObject Target { get; private set; }
    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }

    public PlayerStateMachine(Player player)
    {
        Player = player;
        Target = GameObject.FindGameObjectWithTag("Player");
    }
}
