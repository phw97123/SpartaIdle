using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

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
        Health.OnHit += OnKnockback; 
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

        float curAnimationTime = Animator.GetCurrentAnimatorStateInfo(0).length;
        for (int i = 0; i < childCound; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    private void OnKnockback()
    {
        StartCoroutine(TakeKnockback(1.5f, 2f)); 
    }

    private IEnumerator TakeKnockback(float duration, float force)
    {
        float timer = 0f; 
        while(timer<= duration)
        {

            timer += Time.deltaTime;
            Vector2 direction = (stateMachine.Target.transform.position - transform.position).normalized;
            CharacterRigidbody2D.AddForce(-direction*force,ForceMode2D.Force); 
        }
        yield return 0; 
    }
}
