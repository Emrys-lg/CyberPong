using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float _speed = 5f;
    private Vector2 _moveInput;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!IsOwner || !IsSpawned) return;
        _moveInput = context.ReadValue<Vector2>();
    }

    void Update()
    {
        if (!IsOwner || !IsSpawned) return;
        float multiplier = _speed * Time.deltaTime;
        Vector3 movement = new Vector3(_moveInput.x * multiplier, 0, _moveInput.y * multiplier);
        transform.position += movement;
    }
}
