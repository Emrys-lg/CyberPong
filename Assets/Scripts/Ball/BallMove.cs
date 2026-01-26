using UnityEngine;
using Unity.Netcode;

public class BallMove : NetworkBehaviour
{
    [SerializeField] float _currentMagnitude = 0;
    [SerializeField] float _maxMoveSpeed = 0;

    public void MoveToDirection(Vector3 direction, float impulseStrenght)
    {

        BallMain.Instance.Rb.MovePosition(direction * impulseStrenght);

    }

    public void Bounce(Vector3 direction)
    {
        BallMain.Instance.Rb.linearVelocity = direction * BallMain.Instance.Rb.linearVelocity.magnitude;
    }

    public void Swinged(Vector3 direction, float force)
    {
        _currentMagnitude = force;
        _maxMoveSpeed = force;
        BallMain.Instance.Rb.linearVelocity = direction * _currentMagnitude;
    }

    private void FixedUpdate()
    {
        _currentMagnitude = BallMain.Instance.Rb.linearVelocity.magnitude;
    }

    void DecreaseSpeed()
    {

    }
}
