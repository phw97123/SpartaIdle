using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor.Search;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider2D thisCollider;
    private int damage;
    private float knockback;

    private void OnEnable()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == thisCollider ) return;

        if(collision.TryGetComponent<Health>(out Health health))
        { 
            health.TakeDamage(damage);
            Debug.Log($"µ¥¹ÌÁö : {damage} "); 
        }

        if (collision.TryGetComponent(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (collision.transform.position - thisCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * knockback);
        }
    }

    public void SetAttack(int damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }
}
