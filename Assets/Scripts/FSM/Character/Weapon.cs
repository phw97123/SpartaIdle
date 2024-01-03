using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider2D thisCollider;
    private int damage; 
    
    public List<Collider2D> aleadyCollider = new List<Collider2D>();

    private void OnEnable()
    {
        aleadyCollider.Clear(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == thisCollider ) return;
        if (aleadyCollider.Contains(collision)) return;
        aleadyCollider.Add(collision);
        if(collision.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(damage);
            Debug.Log($"µ¥¹ÌÁö : {damage} "); 
            aleadyCollider.Remove(collision);
        }
    }

    public void SetAttack(int damage)
    {
        this.damage = damage;
    }
}
