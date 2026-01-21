using Unity.Netcode;
using UnityEngine;

public class PlayerCollision : NetworkBehaviour
{
    [SerializeField] PlayerMain PlayerMain;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            PlayerMain.PlayerHealth.TakeDamage(1);
        }
    }
}
