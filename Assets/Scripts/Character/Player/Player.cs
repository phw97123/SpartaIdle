using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator { get; private set; }

    [SerializeField] public CharacterAnimationData AnimationData { get; private set; }
    public CharacterController characterController { get; private set; }

    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        AnimationData = new CharacterAnimationData();
        AnimationData.Initialize();

        //rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();

        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }
    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }
}
