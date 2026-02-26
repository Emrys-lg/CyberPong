using UnityEngine;
using Unity.Netcode;

public class BallMove : NetworkBehaviour
{
    public float MaxMoveSpeed = 15f;
    public float Lerp = 0.4f;

    private NetworkVariable<Vector3> netPos =
        new(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    private Vector3 targetPos;

    public override void OnNetworkSpawn()
    {
        if (!IsServer)
        {
            netPos.OnValueChanged += OnNetPositionChanged;
        }
    }

    void FixedUpdate()
    {
        if (!IsSpawned) return;

        var rb = BallMain.Instance.Rb;

        if (IsServer)
        {
            if (rb.linearVelocity.magnitude > MaxMoveSpeed && BallMain.Instance.IsPowered)
                rb.linearVelocity = rb.linearVelocity.normalized * MaxMoveSpeed;

            netPos.Value = rb.position;
        }
        else
        {
            rb.MovePosition(Vector3.Lerp(rb.position, targetPos, 0.4f));
        }
    }

    void OnNetPositionChanged(Vector3 oldValue, Vector3 newValue)
    {
        targetPos = newValue;
    }

    public void Swing(Vector3 dir, float force)
    {
        if (!IsServer) return;

        var rb = BallMain.Instance.Rb;
        rb.linearVelocity = dir.normalized * force;
    }
}