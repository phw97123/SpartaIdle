using System.Collections;
using UnityEngine;

public class Monster : Character
{
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] ParticleSystem deathEffect; 
    [SerializeField] MonsterFSM FSM;

    public override bool TakeDamage(float value)
    {
        hitEffect.Play();

        if (CheckHealth())
        {
            StartCoroutine(DeathFadeout());
        }
        return base.TakeDamage(value);
    }

    private IEnumerator DeathFadeout()
    {
        deathEffect.Play();

        float fadeDuration = 0.8f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                Color c = prevColor[i];
                c.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                spriteRenderers[i].color = c;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
        InitSprite();
    }

    public void InitSprite()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sprite = initialSprite[i];
            spriteRenderers[i].transform.rotation = Quaternion.identity;
        }
    }
}
