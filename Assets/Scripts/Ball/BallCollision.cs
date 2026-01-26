using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Vector3 direction = (BallMain.Instance.transform.position - other.transform.position).normalized;
            BallMain.Instance.BallMove.MoveToDirection(direction);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
    }
}
