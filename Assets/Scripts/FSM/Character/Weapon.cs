using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider2D thisCollider;
    private int damage;
    private float knockbackDuration;
    private float knockbackForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == thisCollider) return;
        if(thisCollider.CompareTag(collision.tag)) return;

        if (collision.tag == "Enemy")
        {
            if (collision.transform.parent.TryGetComponent<Health>(out Health enemyHealth))
            {
                if (enemyHealth.TakeDamage(damage))
                {
                    Player.Instance.target = null;
                    Player.Instance.stateMachine.Target = null;
                }
            }
        }

        else if (collision.TryGetComponent<Health>(out Health playerHealth))
        {
            if (playerHealth.TakeDamage(damage))
            {
                Player.Instance.target = null;
                Player.Instance.stateMachine.Target = null;
            }
            if (damage > 0 && knockbackForce > 0)
            {
                //Knockback(collision.transform);
            }
        }
    }

    public void SetAttack(int damage, float knockbackForce, float knockbackDuration)
    {
        this.damage = damage;
        this.knockbackForce = knockbackForce;
        this.knockbackDuration = knockbackDuration;
    }

    public void Knockback(Transform obj)
    {
        float timer = 0;
        while (knockbackDuration > timer)
        {
            timer += Time.deltaTime; 
            Vector2 direction = (obj.transform.position - gameObject.transform.position).normalized;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * knockbackForce,ForceMode2D.Force);
        }
    }
}
