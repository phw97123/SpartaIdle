using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public Animator Animator { get; private set; }

    [SerializeField] public CharacterAnimationData AnimationData { get; private set; }
    private Collider2D collider2d; 

    public Rigidbody2D CharacterRigidbody2D { get; private set; }

    private EnemyStateMachine stateMachine;

    public Health Health { get;  set; } 

    public Weapon weapon;

    public bool IsAttackRange { get; set; }

    WaitForSeconds deadTime; 

    private void Awake()
    {
        AnimationData = new CharacterAnimationData();
        AnimationData.Initialize();

        Animator = GetComponent<Animator>();
        CharacterRigidbody2D = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();

        stateMachine = new EnemyStateMachine(this);

        Health = GetComponent<Health>();

        deadTime = new WaitForSeconds(.8f);
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

    private IEnumerator DeadAnimation()
    {
        Animator.SetTrigger("Die");
        int childCound = transform.childCount;

        for (int i = 0; i < childCound; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(false);
        }
        yield return deadTime;
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
