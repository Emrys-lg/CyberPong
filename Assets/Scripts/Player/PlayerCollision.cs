using Unity.Netcode;
using UnityEngine;

public class PlayerCollision : NetworkBehaviour
{
    [SerializeField] private PlayerMain PlayerMain;
    [SerializeField] private float damageCooldown = 0.5f;

    private float _lastDamageTime = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer) return;

        if (other.gameObject.CompareTag("Ball"))
        {
            if (Time.time - _lastDamageTime >= damageCooldown)
            {
                PlayerMain.PlayerHealth.TakeDamage(1);
                _lastDamageTime = Time.time;
            }
        }
    }
}