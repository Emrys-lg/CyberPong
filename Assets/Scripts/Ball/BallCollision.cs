using Unity.Netcode;
using UnityEngine;

public class BallCollision : NetworkBehaviour
{
    [Header("Bounce Settings")]
    [SerializeField] private float randomBounceAngle = 5f;
    [SerializeField] private float minBounceSpeed = 3f;     
    [SerializeField] private float bounceDamping = 0.98f;

    void OnCollisionEnter(Collision collision)
    {
        if (!IsServer) return;
        if (!collision.gameObject.CompareTag("Wall")) return;

        Rigidbody rb = BallMain.Instance.Rb;

        // Get ball velocity before collision
        Vector3 incoming = rb.linearVelocity;
        float incomingSpeed = incoming.magnitude;

        // Get wall surface
        Vector3 normal = collision.contacts[0].normal;

        // Calculate miror direction 
        Vector3 reflectedDir = Vector3.Reflect(incoming.normalized, normal);

        // Random variation
        float angle = Random.Range(-randomBounceAngle, randomBounceAngle);
        reflectedDir = Quaternion.AngleAxis(angle, Vector3.up) * reflectedDir;

        if (BallMain.Instance.IsPowered)
        {
            // Powered mode: always maintain max speed
            rb.linearVelocity = reflectedDir * BallMain.Instance.BallMove.MaxMoveSpeed;
        }
        else
        {
            // If ball is too slow give minBounceSpeed
            float bounceSpeed = Mathf.Max(incomingSpeed, minBounceSpeed);

            // Apply bounce energy loss
            bounceSpeed *= bounceDamping;

            rb.linearVelocity = reflectedDir * bounceSpeed;
        }
    }
}