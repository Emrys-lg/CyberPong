using Unity.Netcode;
using UnityEngine;


public class PlayerMain : NetworkBehaviour
{
    public PlayerMovement PlayerMove;
    public PlayerColor PlayerColor;
    public PlayerHealth PlayerHealth;
    public PlayerUI PlayerUI;
    public PlayerSwing PlayerSwing;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        PlayerColor.SetColorBasedOnOwner();
        NetworkSession.Instance.AddToPlayerCountUI(this.PlayerUI); //ICI A FIX 
        PlayerHealth.SetupHealth();
        PlayerUI.UpdateHealthUIClientRpc();

    }

    protected override void OnOwnershipChanged(ulong previous, ulong current)
    {
        PlayerColor.SetColorBasedOnOwner();
    }
}
