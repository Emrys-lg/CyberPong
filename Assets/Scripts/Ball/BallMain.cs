using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class BallMain : NetworkBehaviour
{
    public BallStateBrain BallStateBrain;
    public BallMove BallMove;
    public BallCollision BallCollision;
    public BallColor BallColor;

    public Rigidbody Rb;

    private static BallMain instance = null;
    public static BallMain Instance => instance;


    public bool IsPowered = false;

    public override void OnNetworkSpawn()
    {
        if (!IsServer)
        {
            Rb.isKinematic = true;
        }
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }


}
