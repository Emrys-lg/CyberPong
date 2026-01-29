using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerUI : NetworkBehaviour
{
    [SerializeField] PlayerMain PlayerMain;
    public TextMeshProUGUI health;

    [ClientRpc]
    public void UpdateHealthUIClientRpc()
    {
        health.text = PlayerMain.PlayerHealth.currentHealth.ToString();
    }
}
