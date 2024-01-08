using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator Animator { get; private set; }

    [SerializeField] public CharacterAnimationData AnimationData { get; private set; }
    public Rigidbody2D CharacterRigidbody2D { get; private set; }
    private Collider2D characterCollider;
    private EnemyStateMachine stateMachine;
    public Health Health { get; set; }

    public Weapon weapon;
    public bool IsAttackRange { get; set; }

    [SerializeField] private ParticleSystem deadEffct;

    private SpriteRenderer[] allSpriteRenderer;
    private Color[] spriteColors;
    private Color[] prevColor;


    private void Awake()
    {
        AnimationData = new CharacterAnimationData();
        AnimationData.Initialize();

        Animator = GetComponent<Animator>();
        CharacterRigidbody2D = GetComponent<Rigidbody2D>();
        characterCollider = GetComponent<Collider2D>();

        stateMachine = new EnemyStateMachine(this);

        Health = GetComponent<Health>();

        stateMachine.ChangeState(stateMachine.IdleState);
        Health.OnDie += OnDie;

        allSpriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        spriteColors = new Color[allSpriteRenderer.Length];
        prevColor = new Color[spriteColors.Length];

        for (int i = 0; i < allSpriteRenderer.Length; i++)
        {
            spriteColors[i] = allSpriteRenderer[i].color;
        }

        deadEffct.Stop();
    }

    public void Init()
    {
        Health.Init();
        characterCollider.enabled = true;
        gameObject.SetActive(true);

        for (int i = 0; i < allSpriteRenderer.Length; i++)
        {
            allSpriteRenderer[i].color = spriteColors[i];
        }
        stateMachine.ChangeState(stateMachine.IdleState); 
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
        StartCoroutine(Fadeout());
    }

    // TODO : 유틸 함수로 빼기 
    private IEnumerator Fadeout()
    {
        deadEffct.Play();

        for (float f = 1.2f; f > 0f; f -= 0.05f) 
        {
            for(int i = 0; i< allSpriteRenderer.Length; i++)
            {
                Color c = spriteColors[i];
                c.a = f;
                allSpriteRenderer[i].color = c; 
            }
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
