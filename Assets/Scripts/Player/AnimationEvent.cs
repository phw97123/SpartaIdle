using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Player player; 

    public void MeleeAttackEvent()
    {
        playerController.MeleeAttackEvent();
    }

    public void StopMeleeAttackAnimationEvent()
    {
        player.StopAnimation(Strings.ANIMATION_MELEEATTACK1);
    }
}
