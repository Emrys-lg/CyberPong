using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwing : NetworkBehaviour
{
    [SerializeField]  PlayerMain _playerMain;
    [SerializeField] GameObject _bat;

    private NetworkVariable<bool> _batActivated = new NetworkVariable<bool>(false);

    private void Update()
    {
        if (!IsOwner) return;

        bool shouldActivate = Mouse.current.leftButton.isPressed;

        _bat.SetActive(shouldActivate);

        if (shouldActivate != _batActivated.Value)
        {
            SetBatStateServerRpc(shouldActivate);
        }
    }

    [ServerRpc]
    private void SetBatStateServerRpc(bool activated)
    {
        _batActivated.Value = activated;
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            _batActivated.OnValueChanged += OnBatStateChanged;
            _bat.SetActive(_batActivated.Value);
        }
    }

    private void OnBatStateChanged(bool oldValue, bool newValue)
    {
        _bat.SetActive(newValue);
    }
}
