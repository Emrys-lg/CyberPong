using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class BallMain : NetworkBehaviour
{
    public BallMove BallMove;

    public Rigidbody Rb;

    private static BallMain instance = null;
    public static BallMain Instance => instance;

    private void Awake()
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
        DontDestroyOnLoad(this.gameObject);
    }


}
