using Unity.Netcode;
using UnityEngine;


public class PlayerMain : NetworkBehaviour
{
    public PlayerMovement PlayerMove;
    public PlayerColor PlayerColor;
    public PlayerHealth PlayerHealth;
    public PlayerUI PlayerUI;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        PlayerColor.SetColorBasedOnOwner();
        PlayerHealth.SetupHealth();
        PlayerUI.UpdateHealthUI();
    }

    protected override void OnOwnershipChanged(ulong previous, ulong current)
    {
        PlayerColor.SetColorBasedOnOwner();
    }
}
