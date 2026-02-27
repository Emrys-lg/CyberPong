using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class NetworkSession : NetworkBehaviour
{
    public static NetworkSession Instance { get; private set; }

    private int _currentPlayerCount = 0;
    private List<PlayerUI> _playerUIs = new List<PlayerUI>();
    [SerializeField] private GameObject tempUI;

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
        _currentPlayerCount++;
        _playerUIs.Add(playerUI);
        //StartCoroutine(DelayedAssignPlayerUI(playerUI.NetworkObjectId, _currentPlayerCount));

        Debug.Log($"Player {_currentPlayerCount} registered");
    }
    private IEnumerator DelayedAssignPlayerUI(ulong playerNetworkObjectId, int playerNumber)
    {
        yield return new WaitUntil(() => NetworkManager.Singleton != null && NetworkManager.Singleton.IsListening);
        AssignPlayerUIClientRpc(playerNetworkObjectId, playerNumber);
    }

    //TO REWORK
    [ClientRpc]
    private void AssignPlayerUIClientRpc(ulong playerNetworkObjectId, int playerNumber)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(playerNetworkObjectId, out NetworkObject networkObject))
        {
            PlayerUI playerUI = networkObject.GetComponent<PlayerUI>();

            if (playerUI != null)
            {
                if (playerNumber == 1)
                {
                    playerUI.health = _player01UIHealth;
                }
                else if (playerNumber == 2)
                {
                    playerUI.health = _player02UIHealth;
                }

                PlayerMain playerMain = networkObject.GetComponent<PlayerMain>();
                if (playerMain != null && playerMain.PlayerHealth != null)
                {
                    playerMain.PlayerHealth.SetupHealth();
                    int currentHP = playerMain.PlayerHealth.currentHealth.Value;
                    playerUI.health.text = currentHP.ToString();
                }
            }
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