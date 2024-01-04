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
    private WaitForSeconds interval = new WaitForSeconds(.3f);

    private void Start()
    {
        health = maxHealth;
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        damageColor = new Color32(150, 0, 24, 255);
    }

    public void Init()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (health == 0) return;
        health = Mathf.Max(health - damage, 0);

        if (damage > 0)
        {
            StartCoroutine(TakeDamageColor());
        }

        if (health == 0)
            OnDie?.Invoke();
    }

    private IEnumerator TakeDamageColor()
    {
        SetColor(damageColor);
        yield return interval;
        SetColor(Color.white);
    }

    private void SetColor(Color color)
    {
        foreach (var renderer in spriteRenderers)
        {
            if (renderer == shadowSprite) continue;

            renderer.color = color;
        }
    }
}
