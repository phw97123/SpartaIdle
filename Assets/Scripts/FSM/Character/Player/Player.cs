using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public CharacterAnimationData AnimationData { get; private set; }

    [field: SerializeField] public PlayerSO Data { get; private set; }
    public Animator Animator { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
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
        AnimationData = new CharacterAnimationData();
        AnimationData.Initialize();

        CharacterRigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
        ForceReceiver = GetComponent<ForceReceiver>();

        Health = GetComponent<Health>();
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            if (closestEnemy == null)
            {
                closestEnemy = collision.gameObject;
                stateMachine.SetTargetEnemy(closestEnemy);
            }
        }
    }
}
