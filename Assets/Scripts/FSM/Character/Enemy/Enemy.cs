using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator Animator { get; private set; }

    [SerializeField] public CharacterAnimationData AnimationData { get; private set; }

    public Rigidbody2D CharacterRigidbody2D { get; private set; }

    private EnemyStateMachine stateMachine;

    public Health Health { get; private set; } 

    public Weapon weapon; 

    private void Awake()
    {
        AnimationData = new CharacterAnimationData();
        AnimationData.Initialize();

        Animator = GetComponent<Animator>();
        CharacterRigidbody2D = GetComponent<Rigidbody2D>();

        stateMachine = new EnemyStateMachine(this);

        Health = GetComponent<Health>();
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
        Health.OnDie += OnDie; 
    }
    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void OnDie()
    {
        Animator.SetTrigger("Die");
        enabled = false; 
        Destroy(gameObject);
    }
}
