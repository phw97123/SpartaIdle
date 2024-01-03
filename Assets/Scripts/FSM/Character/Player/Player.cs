using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator Animator { get; private set; }

    [SerializeField] public CharacterAnimationData AnimationData { get; private set; }
    public Rigidbody2D CharacterRigidbody2D { get; private set; }

    private PlayerStateMachine stateMachine;

    public Health Health { get; private set; }
    public Weapon weapon;


    private WaitForSeconds waitTime = new WaitForSeconds(3.0f);

    private void Awake()
    {
        AnimationData = new CharacterAnimationData();
        AnimationData.Initialize();

        Animator = GetComponentInChildren<Animator>();
        CharacterRigidbody2D = GetComponent<Rigidbody2D>();

        stateMachine = new PlayerStateMachine(this);

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
        StartCoroutine(Resurrection()); 
    }

    public IEnumerator Resurrection()
    {
        yield return waitTime;

        // 플레이어 데이터 초기화 
    }
}
