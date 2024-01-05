using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] Player player;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            player.IsAttackRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            player.IsAttackRange = false;
        }
    }
}
