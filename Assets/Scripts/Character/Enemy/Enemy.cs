using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator { get; private set; }

    [SerializeField] public CharacterAnimationData AnimationData { get; private set; }

    public CharacterController characterController { get; private set; }

    private EnemyStateMachine stateMachine;

    private void Awake()
    {
        AnimationData = new CharacterAnimationData();
        AnimationData.Initialize();

        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        stateMachine = new EnemyStateMachine(this);
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }
    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }
}
