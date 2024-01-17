using UnityEngine;
using MonsterLove.StateMachine;

public class PlayerFSM : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerController playerController;

    private StateMachine<Enums.StateEnum> FSM;

    private const float attackDelay = 1;
    private float lastAttackTime = 0f;
    string currentAnimationName;

    private void Awake()
    {
        FSM = new StateMachine<Enums.StateEnum>(this);
        player = GetComponent<Player>();
        playerController = GetComponent<PlayerController>();
        FSM.ChangeState(Enums.StateEnum.Spawn);
    }

    private void Update()
    {
        FSM.Driver.Update?.Invoke();
    }

    private void FixedUpdate()
    {
        FSM.Driver.FixedUpdate?.Invoke();
    }

    private void Spawn_Enter()
    {
        FSM.ChangeState(Enums.StateEnum.Idle);
    }

    private void Idle_Enter()
    {
        player.StartAnimation(Strings.ANIMATION_IDLE);
        playerController.FindClosestMonster();
    }

    private void Idle_FixedUpdate()
    {
        if (!playerController.CheckClosestMonster())
        {
            playerController.FindClosestMonster();
        }
        else FSM.ChangeState(Enums.StateEnum.Run); 
    }

    private void Idle_Exit()
    {
        player.StopAnimation(Strings.ANIMATION_IDLE);
    }

    private void Run_Enter()
    {
        player.StartAnimation(Strings.ANIMATION_RUN);
    }

    private void Run_Update()
    {
        if (player.isAttacking)
        {
            FSM.ChangeState(Enums.StateEnum.MeleeAttack);
        }
    }

    private void Run_FixedUpdate()
    {
        if (!playerController.Move())
            FSM.ChangeState(Enums.StateEnum.Idle);
    }

    private void Run_Exit()
    {
        playerController.FlipSprite();
        player.StopAnimation(Strings.ANIMATION_RUN); 
    }

    private void MeleeAttack_Enter()
    {
        currentAnimationName = Strings.ANIMATION_MELEEATTACK1;  
    }

    private void MeleeAttack_Update()
    {
        if(Time.time - lastAttackTime >= attackDelay)
        {
            TryAttack(); 
            lastAttackTime = Time.time;
        }
        CheckStateTransition();
    }

    private void MeleeAttack_Exit()
    {
        player.StopAnimation(Strings.ANIMATION_MELEEATTACK1); 
    }

    private void Death_Enter()
    {
        player.SetTriggerAnimation(Strings.ANIMATION_DEATH); 
    }

    private void CheckStateTransition()
    {
        if(!playerController.CheckClosestMonster() || !player.isAttacking || !playerController.CheckClosestMonsterActive())
        {
            FSM.ChangeState(Enums.StateEnum.Idle); 
        }
    }

    private void TryAttack()
    {
        if(Time.time - lastAttackTime >= attackDelay)
        {
            player.StartAnimation(currentAnimationName);
            lastAttackTime = Time.time; 
        }
    }
}
