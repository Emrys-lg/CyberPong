using Unity.Netcode;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    public int MaxHealth = 99;
    public NetworkVariable<int> currentHealth = new NetworkVariable<int>();
    public PlayerMain PlayerMain;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        currentHealth.OnValueChanged += OnHealthChanged;

        OnHealthChanged(0, currentHealth.Value);
    }

    private void OnHealthChanged(int oldValue, int newValue)
    {
        if (PlayerMain != null && PlayerMain.PlayerUI != null && PlayerMain.PlayerUI.health != null)
        {
            PlayerMain.PlayerUI.health.text = newValue.ToString();
        }
    }

    public void SetupHealth()
    {
        if (IsServer)
        {
            currentHealth.Value = MaxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!IsServer) return;

        currentHealth.Value -= damage;

        if (currentHealth.Value < 0)
        {
            currentHealth.Value = 0;
            OnPlayerDeath();
        }
    }

    private void OnPlayerDeath()
    {
    }
}