using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] int monstersDetectedCount = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            monstersDetectedCount++;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        player.IsAttackRange = monstersDetectedCount == 0 ? false : true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            monstersDetectedCount--;
        }
    }
}
