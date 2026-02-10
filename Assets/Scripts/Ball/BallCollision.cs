using Unity.Netcode;
using UnityEngine;

public class BallCollision : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        BallMain.Instance.Rb.isKinematic = false;
    }

    private void FixedUpdate()
    {
        if (BallMain.Instance.Rb.isKinematic)
        {
            BallMain.Instance.Rb.isKinematic = false;
        }
    }
}
