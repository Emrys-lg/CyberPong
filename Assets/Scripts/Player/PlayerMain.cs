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

        if (IsServer && NetworkSession.Instance != null)
        {
            NetworkSession.Instance.RegisterPlayer(PlayerUI);
        }

        PlayerHealth.SetupHealth();
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        if (IsServer && NetworkSession.Instance != null)
        {
            NetworkSession.Instance.UnregisterPlayer(PlayerUI);
        }
    }

    protected override void OnOwnershipChanged(ulong previous, ulong current)
    {
        PlayerColor.SetColorBasedOnOwner();
    }
}