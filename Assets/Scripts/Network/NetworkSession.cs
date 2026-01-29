using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class NetworkSession : NetworkBehaviour
{
    private static NetworkSession instance = null;
    public static NetworkSession Instance => instance;

    [SerializeField] int _maxPlayerCount = 2;
    [SerializeField] int _currentPlayerCount;

    [SerializeField] Canvas _tempCanvas;
    [SerializeField] Canvas _gameCanvas;

    [SerializeField] TextMeshProUGUI _player01UIHealth;
    [SerializeField] TextMeshProUGUI _player02UIHealth;


    public void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void AddToPlayerCountUI(PlayerUI playerUI)
    {
        if (!IsServer) return;
        _currentPlayerCount++;
        playerUI.health = _player01UIHealth;
        if (_currentPlayerCount == 2) playerUI.health = _player02UIHealth;

        //ClientRpcParams clientRpcParams = new ClientRpcParams();
        //SendMessage = new ClientRpcSendParams
        //{
        //    TargetClientIds = new ulong[] {playerClientId}
        //}
        //SwitchUIClientRPC(clientParams);
    }

    [ClientRpc]
    void SwitchUIClientRPC(ClientRpcParams clientParams)
    {
        _tempCanvas.gameObject.SetActive(false);
        _gameCanvas.gameObject.SetActive(true);
    }
}
