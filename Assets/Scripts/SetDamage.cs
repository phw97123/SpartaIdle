using UnityEngine;

public class SetDamage : MonoBehaviour
{
    [SerializeField] Collider2D weaponCollider;

    public void StartAttackNotify()
    {
        weaponCollider.enabled = true;
    }

    public void StopAttackNotify()
    {
        weaponCollider.enabled = false;
    }
}
