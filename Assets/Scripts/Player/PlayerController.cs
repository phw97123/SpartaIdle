using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Action isKilled;

    private Player player;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform closestMonsterTransform;

    // TODO : PlayerData 
    private float moveSpeed = 2;

    private float radius = 0.35f;
    [SerializeField] float angle = 0f;
    [SerializeField] private Vector3 centerPosition;

    [SerializeField] GameObject attackCollider;
    private float tempSpeed = 1;

    private void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        isKilled += ResetClosestMonster;
        centerPosition = transform.position;
        tempSpeed = player.GetAnimationLength(Strings.ANIMATION_MELEEATTACK1) / 2;
    }

    public bool Move()
    {
        if (closestMonsterTransform == null) return false;

        Vector3 position = rb.position;
        Vector3 direction = (closestMonsterTransform.position - position).normalized;

        Vector3 newPosition = position + direction * (moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        FlipSprite(direction.x);
        return true;
    }

    private float elapsedTime = 0f;
    public float totalTime;

    public IEnumerator MeleeAttack()
    {
        totalTime = tempSpeed;
        while (true)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
            if (elapsedTime < totalTime)
            {
                float angle = (elapsedTime / totalTime) * 180f;
                float radian = angle * Mathf.Deg2Rad;

                Vector3 newPosition = centerPosition + new Vector3(Mathf.Sin(radian), Mathf.Cos(radian), 0) * radius;
                attackCollider.transform.localPosition = newPosition;
            }
            else
            {
                elapsedTime = 0;
                yield break;
            }
        }
    }

    public void MeleeAttackEvent()
    {
        StartCoroutine(MeleeAttack());
    }

    public void FindClosestMonster()
    {
        Collider2D[] hitColliders = BattleSystem.GetColliderInCircle(transform.position, 10, 1 << 12);

        float closestDistance = Mathf.Infinity;
        Transform closestPlayer = null;
        foreach (Collider2D hitCollider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = hitCollider.transform;
            }
        }
        closestMonsterTransform = closestPlayer;
    }

    public bool CheckClosestMonster()
    {
        return closestMonsterTransform != null;
    }

    public void ResetClosestMonster()
    {
        closestMonsterTransform = null;
    }

    public bool CheckClosestMonsterActive()
    {
        return closestMonsterTransform.gameObject.activeSelf;
    }

    private void FlipSprite(float directionX)
    {
        if (closestMonsterTransform == null) return;

        Transform transform = this.transform;
        Vector3 scale = transform.localScale;
        Vector3 localScale = new Vector3(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z));

        switch (directionX)
        {
            case > 0.1f:
                localScale = new Vector3(localScale.x, localScale.y, localScale.z);
                transform.localScale = localScale;
                break;

            case < -0.1f:
                localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
                transform.localScale = localScale;
                break;
        }
    }

    public void FlipSprite()
    {
        if (closestMonsterTransform == null) return;

        var scale = transform.localScale;
        var direction = (closestMonsterTransform.position - transform.position).normalized;
        var localScale = new Vector3(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z));

        switch (direction.x)
        {
            case > 0.1f:
                localScale = new Vector3(localScale.x, localScale.y, localScale.z);
                transform.localScale = localScale;
                break;
            case < -0.1f:
                localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
                transform.localScale = localScale;
                break;
        }
    }
}
