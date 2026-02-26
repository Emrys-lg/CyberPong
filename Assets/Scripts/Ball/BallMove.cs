using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;

public class BallMove : NetworkBehaviour
{
    public float MaxMoveSpeed = 15f;
    public float InterpolationBackTime = 0.1f;

    private NetworkVariable<Vector3> netPos =
        new(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    private List<Snapshot> snapshots = new();

    struct Snapshot
    {
        public Vector3 Position;
        public float Time;
    }

    public override void OnNetworkSpawn()
    {
        if (!IsServer)
        {
            netPos.OnValueChanged += (_, newPos) => AddSnapshot(newPos);
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
            float renderTime = Time.time - InterpolationBackTime;

            if (snapshots.Count < 2) return;

            while (snapshots.Count >= 2 && snapshots[1].Time <= renderTime)
                snapshots.RemoveAt(0);

            var s0 = snapshots[0];
            var s1 = snapshots[1];

            float t = Mathf.InverseLerp(s0.Time, s1.Time, renderTime);
            Vector3 pos = Vector3.Lerp(s0.Position, s1.Position, t);

            rb.MovePosition(pos);
        }
    }

    void AddSnapshot(Vector3 pos)
    {
        snapshots.Add(new Snapshot { Position = pos, Time = Time.time });
        if (snapshots.Count > 20) snapshots.RemoveAt(0);
    }

    public void Swing(Vector3 dir, float force)
    {
        if (!IsServer) return;

        var rb = BallMain.Instance.Rb;
        rb.linearVelocity = dir.normalized * force;
    }
}