using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbody2d { get; private set; }
    public Animator animator { get; private set; }

    public PlayerAnimationData AnimationData { get; private set; }
    public CharacterController characterController { get; private set; }

    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();

        stateMachine = new PlayerStateMachine(this);
    }
}
