using Unity.Netcode;
using UnityEngine;

public class BatCollision : NetworkBehaviour
{
    public float batForce = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            BatSwingServerRpc();
        }
    }

    [ServerRpc]
    void BatSwingServerRpc()
    {
        BallMain.Instance.BallStateBrain.SwitchBallState(BallMain.Instance.BallStateBrain._ballPoweredState);
        Vector3 direction = (BallMain.Instance.transform.position - transform.position).normalized;
        BallMain.Instance.BallMove.Swinged(direction, batForce);
    }

}
