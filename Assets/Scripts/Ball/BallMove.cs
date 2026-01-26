using UnityEngine;

public class BallMove : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _maxMoveSpeed;

    public void MoveToDirection(Vector3 direction)
    {
        if (BallMain.Instance.Rb.linearVelocity.x >= _maxMoveSpeed || BallMain.Instance.Rb.linearVelocity.y >= _maxMoveSpeed)
        {
            return;
        }
        BallMain.Instance.Rb.linearVelocity = direction * _moveSpeed;

    }

    void DecreaseSpeed()
    {

    }
}
