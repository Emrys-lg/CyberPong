using UnityEngine;
using Unity.Netcode;

public class BallMove : NetworkBehaviour
{
    [SerializeField] float _currentMagnitude = 0;
    [SerializeField] float _maxMoveSpeed = 0;

    public void Swinged(Vector3 direction, float force)
    {
        _currentMagnitude = force;
        _maxMoveSpeed = force;
        BallMain.Instance.Rb.linearVelocity = direction * _currentMagnitude;
    }
}
