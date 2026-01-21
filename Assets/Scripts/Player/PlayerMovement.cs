using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] float speed;
    void Update()
    {
        if (!IsOwner || !IsSpawned) return;
        var multiplier = speed * Time.deltaTime;

        if (Keyboard.current.aKey.isPressed)
        {
            transform.position += new Vector3(-multiplier, 0, 0);
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            transform.position += new Vector3(multiplier, 0, 0);
        }
        else if (Keyboard.current.wKey.isPressed)
        {
            transform.position += new Vector3(0, 0, multiplier);
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            transform.position += new Vector3(0, 0, -multiplier);
        }
    }
}
