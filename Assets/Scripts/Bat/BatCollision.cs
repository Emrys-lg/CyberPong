using Unity.Netcode;
using UnityEngine;

public class BatCollision : NetworkBehaviour
{
    [SerializeField] private float batForce = 20f;

    void OnTriggerEnter(Collider other)
    {
        if (!IsSpawned || !IsServer) return;

        if (other.CompareTag("Ball"))
        {
            if (BallMain.Instance != null)
            {
                Vector3 direction = (other.transform.position - transform.position).normalized;
                BallMain.Instance.Rb.linearVelocity = direction * batForce;
            }
            BatSwingServerRpc(other.transform.position, transform.position);
        }
    }

    [Rpc(SendTo.Server)]
    void BatSwingServerRpc(Vector3 ballPosition, Vector3 batPosition)
    {
        if (BallMain.Instance == null) return;

        Vector3 direction = (ballPosition - batPosition).normalized;

        BallMain.Instance.BallStateBrain.SwitchBallState(
            BallMain.Instance.BallStateBrain._ballPoweredState
        );
        BallMain.Instance.BallMove.Swing(direction, batForce);
    }
}