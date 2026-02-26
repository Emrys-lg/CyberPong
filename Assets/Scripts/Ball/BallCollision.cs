using Unity.Netcode;
using UnityEngine;

public class BallCollision : NetworkBehaviour
{
    [SerializeField] private float randomBounceAngle = 5f;

    void OnCollisionEnter(Collision collision)
    {
        if (!IsServer) return;
        if (!collision.gameObject.CompareTag("Wall")) return;

        Rigidbody rb = BallMain.Instance.Rb;

        Vector3 incoming = rb.linearVelocity;
        Vector3 normal = collision.contacts[0].normal;

        Vector3 reflectedDir = Vector3.Reflect(incoming.normalized, normal);
        float angle = Random.Range(-randomBounceAngle, randomBounceAngle);
        reflectedDir = Quaternion.AngleAxis(angle, Vector3.up) * reflectedDir;

        if (BallMain.Instance.IsPowered)
            rb.linearVelocity = reflectedDir * BallMain.Instance.BallMove.MaxMoveSpeed;
        else
            rb.linearVelocity = reflectedDir * incoming.magnitude;
    }
}