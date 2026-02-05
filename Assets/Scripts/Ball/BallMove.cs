using UnityEngine;
using Unity.Netcode;

public class BallMove : NetworkBehaviour
{
    private float _currentMagnitude;
    public float MaxMoveSpeed = 15;

    public float AverageCurrentVelocity;
    [SerializeField] float _swingForce = 0;

    public void Swinged(Vector3 direction, float force)
    {
        _currentMagnitude = force;
        _swingForce = force;
        BallMain.Instance.Rb.linearVelocity = direction * _swingForce;
        _currentMagnitude = BallMain.Instance.Rb.linearVelocity.magnitude;
    }

    void Update()
    {
        if(BallMain.Instance.IsPowered && BallMain.Instance.Rb.linearVelocity.magnitude < _currentMagnitude)
        {
            BallMain.Instance.Rb.linearVelocity = BallMain.Instance.Rb.linearVelocity.normalized * _currentMagnitude;
        }
        if(BallMain.Instance.Rb.linearVelocity.magnitude > MaxMoveSpeed)
        {
            BallMain.Instance.Rb.linearVelocity = BallMain.Instance.Rb.linearVelocity.normalized * MaxMoveSpeed;
        }
        AverageCurrentVelocity = Mathf.Abs((BallMain.Instance.Rb.linearVelocity.x + BallMain.Instance.Rb.linearVelocity.y + BallMain.Instance.Rb.linearVelocity.z)/3);
    }
}
