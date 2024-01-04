using UnityEngine;

[CreateAssetMenu()]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField][field: Range(0f, 50f)] public float BaseSpeed { get; private set; } = 5f;
    [field: SerializeField] public float AttackRange { get; private set; } = 1.5f;
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField][field: Range(-10f, 10f)] public float Force { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_End_TransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 3f)] public float ForceTransitionTime { get; private set; }
}
