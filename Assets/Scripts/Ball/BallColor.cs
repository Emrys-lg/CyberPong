using Unity.Netcode;
using UnityEngine;

public class BallColor : NetworkBehaviour
{
    [SerializeField] Renderer _ballRenderer;

    [ClientRpc]
    public void SetBallColorClientRpc(Color newColor)
    {
        _ballRenderer.material.color = newColor;
    }
}
