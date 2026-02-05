using UnityEngine;
using Unity.Netcode;

public class BallMove : NetworkBehaviour
{
    private float _currentMagnitude;
    public float MaxMoveSpeed = 15;

    public float AverageCurrentVelocity;
    [SerializeField] float _maxcurrentMoveSpeed = 0;

    public void Swinged(Vector3 direction, float force)
    {
        _currentMagnitude = force;
        _maxcurrentMoveSpeed = force;
        BallMain.Instance.Rb.linearVelocity = direction * _currentMagnitude;
    }

    void Update()
    {
        AverageCurrentVelocity = Mathf.Abs((BallMain.Instance.Rb.linearVelocity.x + BallMain.Instance.Rb.linearVelocity.y + BallMain.Instance.Rb.linearVelocity.z)/3);
    }
}
