using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwing : NetworkBehaviour
{
    [SerializeField] PlayerMain _playerMain;
    [SerializeField] GameObject _bat;
    bool shouldActivate = false;

    private NetworkVariable<bool> _batActivated = new NetworkVariable<bool>(true);

    private void Update()
    {
        if (!IsOwner || !IsSpawned) return;
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


    public void OnSwing(InputAction.CallbackContext context)
    {
        if (!IsOwner || !IsSpawned) return;
        shouldActivate = System.Convert.ToBoolean(context.ReadValue<float>());
        _bat.SetActive(shouldActivate);
        SetBatStateServerRpc(shouldActivate);
    }
}
