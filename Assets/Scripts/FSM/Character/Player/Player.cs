using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance; 
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

    public Dictionary<string, float> animationLengths = new Dictionary<string, float>();

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        AnimationData = new CharacterAnimationData();
        AnimationData.Initialize();

        CharacterRigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();

        Health = GetComponent<Health>();
        playerData = new PlayerData(Health); 
        stateMachine = new PlayerStateMachine(this);

        InitializeAnimationLengths();

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

    private void InitializeAnimationLengths()
    {
        AnimationClip[] clips = Animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            animationLengths[clip.name] = clip.length;
        }
    }

    public float GetAnimationLength(string animationName)
    {
        if (animationLengths.TryGetValue(animationName, out float length))
        {
            return length;
        }
        else
        {
            Debug.LogWarning("Animation not found: " + animationName);
            return 0f;
        }
    }
}
