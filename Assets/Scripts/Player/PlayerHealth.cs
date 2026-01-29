using Unity.Netcode;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    [SerializeField] PlayerMain PlayerMain;

    [SerializeField] int maxHealth;
    public int currentHealth;

    public void SetupHealth()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth = currentHealth - damageAmount;
        PlayerMain.PlayerUI.UpdateHealthUIClientRpc();
    }
}
