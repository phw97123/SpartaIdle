using UnityEngine;

public class SetDamage : MonoBehaviour
{
    [SerializeField] Collider2D weaponCollider;

    public void StartAttackNotify()
    {
        weaponCollider.gameObject.SetActive(true);
    }

    public void StopAttackNotify()
    {
        weaponCollider.gameObject.SetActive(false);
    }
}
