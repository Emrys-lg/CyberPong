using UnityEngine;

public class BatCollision : MonoBehaviour
{
    public float batForce;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            BatSwing();
        }
    }

    void BatSwing()
    {
        Vector3 direction = (BallMain.Instance.transform.position - transform.position).normalized;
        BallMain.Instance.BallMove.MoveToDirection(direction);
    }

}
