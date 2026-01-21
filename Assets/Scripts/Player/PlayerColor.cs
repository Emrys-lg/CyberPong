using Unity.Netcode;
using UnityEngine;

public class PlayerColor : NetworkBehaviour
{
    [SerializeField] Renderer playerRenderer;
    public void SetColorBasedOnOwner()
    {
        UnityEngine.Random.InitState((int)OwnerClientId);
        playerRenderer.material.color = UnityEngine.Random.ColorHSV();
    }
}
