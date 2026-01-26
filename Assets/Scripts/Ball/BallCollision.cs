using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (BallMain.Instance.IsPowered)
            {
                Debug.Log("J'explose");
            }
            return;
        }

        else if (other.CompareTag("Wall"))
        {
            Vector3 direction = (BallMain.Instance.transform.position - other.transform.position).normalized;
            BallMain.Instance.BallMove.Bounce(direction);
        }

    }
    //private void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("Exit");
    //}
}
