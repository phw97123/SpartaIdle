using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    private int health;
    public event Action OnDie;

    public bool IsDead => health == 0;

    [Header("ChangeColor")]
    [SerializeField] private GameObject character;
    [SerializeField] private SpriteRenderer shadowSprite;

    private Color damageColor;

    private SpriteRenderer[] spriteRenderers;
    private Color[] prevColor;

    private WaitForSeconds interval = new WaitForSeconds(.3f);

    [SerializeField] ParticleSystem hitEffect;

    private void Start()
    {
        health = maxHealth;
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        prevColor = new Color[spriteRenderers.Length];
        damageColor = new Color32(150, 0, 24, 255);

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            prevColor[i] = spriteRenderers[i].color;
        }
    }

    public void Init()
    {
        health = maxHealth;
    }

    public bool TakeDamage(int damage)
    {
        if (health == 0) return true;
        health = Mathf.Max(health - damage, 0);

        if (damage > 0)
        {
            StartCoroutine(TakeDamageColor());
            if (hitEffect != null)
            {
                hitEffect.Play();
            }
        }

        if (health == 0)
        {
            OnDie?.Invoke();
            return true;
        }

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
}
