using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance; 

    [SerializeField] public CharacterAnimationData AnimationData { get; private set; }

    [field: SerializeField] public PlayerSO Data { get; private set; }

    public PlayerData playerData; 
    public Animator Animator { get; private set; }
    public Rigidbody2D CharacterRigidbody2D { get; private set; }

    public PlayerStateMachine stateMachine;

    public Weapon weapon;

    public GameObject closestEnemy = null;
    public Health Health { get; private set; }

    public bool IsAttackRange {  get;  set; }
    public bool isAttack; 

    public Health target; 

    private void Awake()
    {
        instance = this;


        AnimationData = new CharacterAnimationData();
        AnimationData.Initialize();

        CharacterRigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();

        Health = GetComponent<Health>();
        playerData = new PlayerData(Health); 
        stateMachine = new PlayerStateMachine(this);

        target = stateMachine.Target; 
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
    }
}
