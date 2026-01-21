using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerUI : NetworkBehaviour
{
    [SerializeField] PlayerMain PlayerMain;
    [SerializeField] TextMeshProUGUI health;

    public void UpdateHealthUI()
    {
        health.text = PlayerMain.PlayerHealth.currentHealth.ToString();
    }
}
