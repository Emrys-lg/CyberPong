using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : NetworkBehaviour
{
    [SerializeField] Vector3 _lookDirection;
    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);
        Physics.Raycast(ray, out hit);

        if (hit.collider != null)
        {
            Vector3 _mouseLookDirection = hit.point;
            _mouseLookDirection.y = transform.position.y;
            _lookDirection = _mouseLookDirection;
            transform.LookAt(_mouseLookDirection);
        }
    }


}
