using UnityEngine;

[CreateAssetMenu()]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public float BaseSpeed { get; private set; } = 5f;
    [field: SerializeField] public float AttackRange { get; private set; } = 1.5f;
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float Force { get; private set; }
    [field: SerializeField] public float KnckbackDuration { get; private set; }
}
