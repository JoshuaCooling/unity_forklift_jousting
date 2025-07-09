using UnityEngine;

public class ForkliftAI : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5f;
    public float turnSpeed = 3f;

    public Transform forks; // Assign in Inspector (child of forklift)
    public float forkLiftSpeed = 2f;
    public float maxForkHeight = 1.5f;
    private float originalForkHeight;
    public float flipRange = 4f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (forks != null)
            originalForkHeight = forks.localPosition.y;
    }

    void FixedUpdate()
    {
        if (target == null) return;

        // Move logic
        Vector3 direction = (target.position - transform.position);
        direction.y = 0f;
        direction.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime));

        rb.MovePosition(rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime);

        // Flip logic
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget < flipRange)
        {
            RaiseForks();
        }
        else
        {
            LowerForks();
        }
    }

    void RaiseForks()
    {
        if (forks.localPosition.y < maxForkHeight)
        {
            forks.localPosition += new Vector3(0, forkLiftSpeed * Time.fixedDeltaTime, 0);
        }
    }

    void LowerForks()
    {
        if (forks.localPosition.y > originalForkHeight)
        {
            forks.localPosition -= new Vector3(0, forkLiftSpeed * Time.fixedDeltaTime, 0);
        }
    }
}
