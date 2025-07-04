using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody rb;

    public void Initialize(float speed, float angle, float mass, Vector3 launchDirection)
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on projectile.");
            return;
        }

        rb.isKinematic = false;
        rb.mass = mass;

        Vector3 dir = Quaternion.Euler(-angle, 0, 0) * launchDirection.normalized;
        rb.linearVelocity = dir * speed;
    }
}
