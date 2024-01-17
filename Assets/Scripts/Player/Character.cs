using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator; 

    private Dictionary<string, float> animationLengths = new Dictionary<string, float>();

    public string EnemyTag;
    public bool isAttacking = false;
    protected float maxHealth = 100;
    protected float currentHealth = 0;

    protected Sprite[] initialSprite;
    [SerializeField] protected SpriteRenderer[] spriteRenderers;
    [SerializeField] private SpriteRenderer shadowSprite;
    private Color damageColor;
    protected Color[] prevColor;

    private WaitForSeconds interval = new WaitForSeconds(.3f);

    private void Awake()
    {
        InitializeAnimationLengths();
        currentHealth = maxHealth;
        prevColor = new Color[spriteRenderers.Length];
        damageColor = new Color32(150, 0, 24, 255);

        initialSprite = new Sprite[spriteRenderers.Length];
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            initialSprite[i] = spriteRenderers[i].sprite;
            prevColor[i] = spriteRenderers[i].color;
        }
    }

    private void Start()
    {
        //prevColor = new Color[spriteRenderers.Length];
        //damageColor = new Color32(150, 0, 24, 255);

        //initialSprite = new Sprite[spriteRenderers.Length];
        //for (int i = 0; i < spriteRenderers.Length; i++)
        //{
        //    initialSprite[i] = spriteRenderers[i].sprite;
        //    prevColor[i] = spriteRenderers[i].color;
        //}
    }

    public virtual void Init()
    {
        currentHealth = maxHealth;
    }

    public virtual bool TakeDamage(float value)
    {
        currentHealth = (currentHealth - value) <= 0? 0:(currentHealth - value);

        StartCoroutine(TakeDamageColor()); 

        if (currentHealth == 0) return true;
        return false; 
    }

    private IEnumerator TakeDamageColor()
    {
        foreach (var renderer in spriteRenderers)
        {
            if (renderer == shadowSprite)
                continue;
            renderer.color = damageColor;
        }

        yield return interval;

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = prevColor[i];
        }
    }

    public bool CheckHealth()
    {
        return currentHealth == 0; 
    }

    private void InitializeAnimationLengths()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips; 
        foreach(AnimationClip clip in clips)
        {
            animationLengths[clip.name] = clip.length; 
        }
    }

    public float GetAnimationLength(string animationName)
    {
        if(animationLengths.TryGetValue(animationName, out float length)) return length;
        else
        {
            Debug.LogWarning("Animation not found: " + animationName);
            return 0f;
        }
    }

    public void StartAnimation(string animationName)
    {
        animator.SetBool(animationName, true); 
    }

    public void StopAnimation(string animationName)
    {
        animator.SetBool(animationName, false); 
    }

    public void SetTriggerAnimation(string animationName)
    {
        animator.SetTrigger(animationName); 
    }
}
