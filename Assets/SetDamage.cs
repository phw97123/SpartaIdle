using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDamage : MonoBehaviour
{
    [SerializeField] Collider2D weaponCollider;

    private void Start()
    {
        weaponCollider.enabled = false;
    }

    public void StartAttackNotify()
    {
        weaponCollider.GetComponent<Collider2D>().enabled = true;
    }

    public void DeActiveCollider()
    {
        weaponCollider.GetComponent<Collider2D>().enabled = false;
    }
}
