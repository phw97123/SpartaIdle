using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public Animator Animator { get; private set; }

    [SerializeField] public CharacterAnimationData AnimationData { get; private set; }
    public Rigidbody2D CharacterRigidbody2D { get; private set; }
    private Collider2D characterCollider;  
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
        characterCollider = GetComponent<Collider2D>();

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
        characterCollider.enabled = true;

        stateMachine.ChangeState(stateMachine.IdleState);
        int childCound = transform.childCount;
        for (int i = 0; i < childCound; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(true);
        }

        stateMachine.Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void OnDie()
    {
        characterCollider.enabled = false; 
        StartCoroutine(DeadAnimation());
    }

    private IEnumerator DeadAnimation()
    {
        int childCound = transform.childCount;
        for (int i = 0; i < childCound; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(false);
        }
        yield return deadTime;
        gameObject.SetActive(false);
    }
}
