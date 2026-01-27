using Unity.Netcode;
using UnityEngine;

public class BallCollision : NetworkBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (!IsServer) return; 
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("Exit");
    //}
}
