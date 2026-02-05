using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class NetworkSession : NetworkBehaviour
{
    public static NetworkSession Instance { get; private set; }

    private int _currentPlayerCount = 0;
    private List<PlayerUI> _playerUIs = new List<PlayerUI>();

    [Header("UI Health Texts")]
    public TextMeshProUGUI _player01UIHealth;
    public TextMeshProUGUI _player02UIHealth;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterPlayer(PlayerUI playerUI)
    {
        if (!IsServer) return;

        _currentPlayerCount++;
        _playerUIs.Add(playerUI);
        AssignPlayerUIClientRpc(playerUI.NetworkObjectId, _currentPlayerCount);

        Debug.Log($"Player {_currentPlayerCount} registered");
    }

    [ClientRpc]
    private void AssignPlayerUIClientRpc(ulong playerNetworkObjectId, int playerNumber)
    {
        // Trouver le NetworkObject du joueur
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(playerNetworkObjectId, out NetworkObject networkObject))
        {
            PlayerUI playerUI = networkObject.GetComponent<PlayerUI>();

            if (playerUI != null)
            {
                if (playerNumber == 1)
                {
                    playerUI.health = _player01UIHealth;
                    Debug.Log($"Player 1 UI assigned to {_player01UIHealth.name}");
                }
                else if (playerNumber == 2)
                {
                    playerUI.health = _player02UIHealth;
                    Debug.Log($"Player 2 UI assigned to {_player02UIHealth.name}");
                }

                PlayerMain playerMain = networkObject.GetComponent<PlayerMain>();
                if (playerMain != null && playerMain.PlayerHealth != null)
                {
                    int currentHP = playerMain.PlayerHealth.currentHealth.Value;
                    playerUI.health.text = currentHP.ToString();
                }
            }
            else
            {
                Debug.LogError($"PlayerUI component not found on NetworkObject {playerNetworkObjectId}");
            }
        }
        else
        {
            Debug.LogError($"NetworkObject with ID {playerNetworkObjectId} not found in SpawnedObjects");
        }
    }

    public void UnregisterPlayer(PlayerUI playerUI)
    {
        if (!IsServer) return;

        _playerUIs.Remove(playerUI);
        _currentPlayerCount--;

        Debug.Log($"Player unregistered. Current count: {_currentPlayerCount}");
    }
}