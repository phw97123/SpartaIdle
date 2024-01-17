using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private int enemyCount = 0;

    private void Awake()
    {
        character = transform.parent.GetComponent<Character>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(character.EnemyTag))
        {
            enemyCount++;
            character.isAttacking = enemyCount == 0 ? false : true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(character.EnemyTag))
        {
            enemyCount--;
            character.isAttacking = enemyCount == 0 ? false : true;
        }
    }
}
