using UnityEngine;
using Unity.Netcode;

public class BallMove : NetworkBehaviour
{
    [Header("Speed Settings")]
    public float MaxMoveSpeed = 15f;

    [Header("Debug Info")]
    public float AverageCurrentVelocity;

    private float _currentMagnitude;
    private float _swingForce;

    private NetworkVariable<Vector3> netVelocity = new NetworkVariable<Vector3>(
        Vector3.zero,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    private NetworkVariable<Vector3> netPosition = new NetworkVariable<Vector3>(
        Vector3.zero,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    public override void OnNetworkSpawn()
    {
        if (!IsServer)
        {
            netVelocity.OnValueChanged += OnVelocityChanged;
            netPosition.OnValueChanged += OnPositionChanged;
        }
    }

    public void Swinged(Vector3 direction, float force)
    {
        if (!IsServer) return;

        _currentMagnitude = force;
        _swingForce = force;

        BallMain.Instance.Rb.linearVelocity = direction * _swingForce;
        _currentMagnitude = BallMain.Instance.Rb.linearVelocity.magnitude;

        netVelocity.Value = BallMain.Instance.Rb.linearVelocity;
        netPosition.Value = BallMain.Instance.Rb.position;
    }

    void FixedUpdate()
    {
        if (!IsSpawned) return;

        if (IsServer)
        {
            if (BallMain.Instance.IsPowered &&
                BallMain.Instance.Rb.linearVelocity.magnitude < _currentMagnitude)
            {
                BallMain.Instance.Rb.linearVelocity =
                    BallMain.Instance.Rb.linearVelocity.normalized * _currentMagnitude;
            }

            if (BallMain.Instance.Rb.linearVelocity.magnitude > MaxMoveSpeed)
            {
                BallMain.Instance.Rb.linearVelocity =
                    BallMain.Instance.Rb.linearVelocity.normalized * MaxMoveSpeed;
            }

            netVelocity.Value = BallMain.Instance.Rb.linearVelocity;
            netPosition.Value = BallMain.Instance.Rb.position;
        }
        else
        {
            if (BallMain.Instance.IsPowered &&
                BallMain.Instance.Rb.linearVelocity.magnitude < _currentMagnitude)
            {
                BallMain.Instance.Rb.linearVelocity =
                    BallMain.Instance.Rb.linearVelocity.normalized * _currentMagnitude;
            }

            if (BallMain.Instance.Rb.linearVelocity.magnitude > MaxMoveSpeed)
            {
                BallMain.Instance.Rb.linearVelocity =
                    BallMain.Instance.Rb.linearVelocity.normalized * MaxMoveSpeed;
            }
        }

        UpdateDebugInfo();
    }

    void Update()
    {
        UpdateDebugInfo();
    }

    private void UpdateDebugInfo()
    {
        if (BallMain.Instance?.Rb != null)
        {
            AverageCurrentVelocity = BallMain.Instance.Rb.linearVelocity.magnitude;
        }
    }

    private void OnVelocityChanged(Vector3 oldVel, Vector3 newVel)
    {
        if (IsServer) return;
        if (BallMain.Instance?.Rb == null) return;
        if (BallMain.Instance.Rb.isKinematic) return;

        float velocityDiff = Vector3.Distance(BallMain.Instance.Rb.linearVelocity, newVel);

        if (velocityDiff > 5f)
        {
            BallMain.Instance.Rb.linearVelocity = newVel;
        }
        else if (velocityDiff > 1f)
        {
            BallMain.Instance.Rb.linearVelocity = Vector3.Lerp(
                BallMain.Instance.Rb.linearVelocity,
                newVel,
                0.5f
            );
        }
    }

    private void OnPositionChanged(Vector3 oldPos, Vector3 newPos)
    {
        if (IsServer) return;
        if (BallMain.Instance?.Rb == null) return;

        float positionDiff = Vector3.Distance(BallMain.Instance.Rb.position, newPos);

        if (positionDiff > 2f)
        {
            BallMain.Instance.Rb.position = newPos;
        }
        else if (positionDiff > 0.5f) 
        {
            BallMain.Instance.Rb.position = Vector3.Lerp(
                BallMain.Instance.Rb.position,
                newPos,
                0.3f
            );
        }
    }
}