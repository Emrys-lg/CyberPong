using UnityEngine;

public class BallMove : MonoBehaviour
{
    [SerializeField] float _moveSpeed;

    public void MoveToDirection(Vector3 direction)
    {
        BallMain.Instance.Rb.linearVelocity = direction * _moveSpeed;
    }

    void DecreaseSpeed()
    {

    }
}
