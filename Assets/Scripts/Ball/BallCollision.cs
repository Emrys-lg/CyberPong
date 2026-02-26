using Unity.Netcode;
using UnityEngine;
using System.Collections;

public class BallCollision : NetworkBehaviour
{
    [Header("Bounce Variation")]
    [SerializeField] private float randomBounceAngle = 5f;

    void OnCollisionEnter(Collision collision)
    {
        if (!IsServer) return;

        if (collision.gameObject.CompareTag("Wall"))
        {
            StartCoroutine(TweakBounceDirection());
        }
    }

    IEnumerator TweakBounceDirection()
    {
        yield return new WaitForFixedUpdate();

        Rigidbody rb = BallMain.Instance.Rb;
        Vector3 velocity = rb.linearVelocity;
        float speed = velocity.magnitude;

        float randomAngle = Random.Range(-randomBounceAngle, randomBounceAngle);
        Vector3 newDirection = Quaternion.Euler(0, randomAngle, 0) * velocity.normalized;

        rb.linearVelocity = newDirection * speed;
    }

}