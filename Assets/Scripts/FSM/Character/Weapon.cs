using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider2D thisCollider;
    private int damage;

    private void OnEnable()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == thisCollider) return;
        if(thisCollider.CompareTag(collision.tag)) return;

        if (collision.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(damage);
        }
    }

    public void SetAttack(int damage)
    {
        this.damage = damage;
    }
}
