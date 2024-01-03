using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private float drag = 0.3f;

    private Vector2 dampingVelocity;
    private Vector2 impact;

    public Vector2 Movement => impact;

    private void Update()
    {
        impact = Vector2.SmoothDamp(impact, Vector2.zero, ref dampingVelocity, drag);
    }

    public void Reset()
    {
        impact = Vector2.zero;
    }

    public void AddForce(Vector2 force)
    {
        impact += force;
    }
}
