using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator Animator { get; private set; }

    [SerializeField] public CharacterAnimationData AnimationData { get; private set; }

    public Rigidbody2D CharacterRigidbody2D { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }

    private EnemyStateMachine stateMachine;

    public Health Health { get;  set; } 

    public Weapon weapon;


    private void Awake()
    {
        AnimationData = new CharacterAnimationData();
        AnimationData.Initialize();

        Animator = GetComponent<Animator>();
        CharacterRigidbody2D = GetComponent<Rigidbody2D>();
        ForceReceiver = GetComponent<ForceReceiver>();

        stateMachine = new EnemyStateMachine(this);

        Health = GetComponent<Health>();
    }

    public void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
        Health.OnDie += OnDie;
    }

    public void Init()
    {
        Health.Init();
        stateMachine.ChangeState(stateMachine.IdleState);
        int childCound = transform.childCount;
        for (int i = 0; i < childCound; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void OnDie()
    {
        StartCoroutine(DeadAnimation()); 
    }

    IEnumerator DeadAnimation()
    {
        Animator.SetTrigger("Die");
        int childCound = transform.childCount;

        float curAnimationTime = Animator.GetCurrentAnimatorStateInfo(0).length;
        for (int i = 0; i < childCound; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
