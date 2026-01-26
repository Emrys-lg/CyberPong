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

        else if (other.CompareTag("Bat"))
        {
            BallMain.Instance.BallStateBrain.SwitchBallState(BallMain.Instance.BallStateBrain._ballPoweredState);
        }
        Vector3 direction = (BallMain.Instance.transform.position - other.transform.position).normalized;
        BallMain.Instance.BallMove.MoveToDirection(direction);
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("Exit");
    //}
}
