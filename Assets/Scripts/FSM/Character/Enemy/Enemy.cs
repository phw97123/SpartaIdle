using System;
using System.Collections;
using Unity.VisualScripting;
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

    [SerializeField] private SpriteRenderer[] allSpriteRenderer;
    [SerializeField] private Sprite[] initialSpriteRenderer;

    private Color[] spriteColors;

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
        initialSpriteRenderer = new Sprite[allSpriteRenderer.Length];

        for (int i = 0; i < allSpriteRenderer.Length; i++)
        {
            initialSpriteRenderer[i] = allSpriteRenderer[i].sprite;
            spriteColors[i] = allSpriteRenderer[i].color;
        }

        deadEffct.Stop();
    }

    public void Init()
    {
        Health.Init();
        characterCollider.enabled = true;

        for (int i = 0; i < allSpriteRenderer.Length; i++)
        {
            allSpriteRenderer[i].color = spriteColors[i];
        }
        //InitSprite();
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

    private IEnumerator Fadeout()
    {
        deadEffct.Play();

        float fadeDuration = 0.8f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            for (int i = 0; i < allSpriteRenderer.Length; i++)
            {
                Color c = spriteColors[i];
                c.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                allSpriteRenderer[i].color = c;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
        InitSprite();

    }

    public void InitSprite()
    {
        for (int i = 0; i < allSpriteRenderer.Length; i++)
        {
            allSpriteRenderer[i].sprite = initialSpriteRenderer[i];
            allSpriteRenderer[i].transform.rotation = Quaternion.identity;
        }
    }
}
