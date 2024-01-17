using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Transform closestPlayerTransform;

    [SerializeField] private Rigidbody2D rb;

    private int moveSpeed = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        closestPlayerTransform = GameObject.FindWithTag("Player").transform;
    }

    public void SetClosestPlayer(Transform transform)
    {
        closestPlayerTransform = transform;
    }

    public bool Move()
    {
        if (closestPlayerTransform == null) return false;

        Vector3 position = rb.position;
        var direction = (closestPlayerTransform.position - position).normalized;

        var newPosition = position + direction * (moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        FlipSprite(direction.x);

        return true;
    }
    
    private void FlipSprite(float directionX)
    {
        if (closestPlayerTransform == null) return;

        var transform = this.transform;
        var scale = transform.localScale;
        var localScale = new Vector3(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z));

        switch (directionX)
        {
            case > 0.1f:
                localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
                transform.localScale = localScale;
                break;
            case < -0.1f:
                localScale = new Vector3(localScale.x, localScale.y, localScale.z);
                transform.localScale = localScale;
                break;
        }
    }
}
