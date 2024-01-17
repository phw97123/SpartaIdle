using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(Strings.TAG_MONSTER)) return; 

        // TODO : PlayerData Attack
        if(collision.GetComponent<Monster>().TakeDamage(50))
        {
            PlayerController.isKilled?.Invoke();
        }
    }
}
