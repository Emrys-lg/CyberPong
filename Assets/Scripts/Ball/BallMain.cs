using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class BallMain : NetworkBehaviour
{
    public BallStateBrain BallStateBrain;
    public BallMove BallMove;
    public BallCollision BallCollision;
    public BallVisuals BallVisual;

    public Rigidbody Rb;

    private static BallMain instance = null;
    public static BallMain Instance => instance;


    public bool IsPowered = false;

    public void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            Rb.isKinematic = false;
            Rb.detectCollisions = true;
            Rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
        else
        {
            Rb.isKinematic = true;
            Rb.detectCollisions = false;
        }
    }


}
