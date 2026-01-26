using Unity.Netcode;
using UnityEngine;

public abstract class BallState : NetworkBehaviour
{
    public bool IsComplete { get; protected set; }

    protected float startTime;

    public float time => Time.time - startTime;

    public virtual void OnEnter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void OnExit() { }
}
