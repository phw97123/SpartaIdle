using System;
using UnityEngine;

[Serializable]
public class CharacterAnimationData 
{
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string runParameterName = "Run";

    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string baseAttackParameterName = "BaseAttack";

    [SerializeField] private string hitParameterName = "Hit";
    [SerializeField] private string deadParameterName = "Dead"; 

    public int IdleParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int BaseAttackParameterHash { get; private set; }

    public int HitParameterHash { get; private set; }
    public int DeadParameterHash {  get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        BaseAttackParameterHash = Animator.StringToHash(baseAttackParameterName);
        HitParameterHash = Animator.StringToHash(hitParameterName);
        DeadParameterHash = Animator.StringToHash(deadParameterName);
    }
}


