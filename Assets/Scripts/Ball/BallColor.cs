using Unity.Netcode;
using UnityEngine;

public class BallColor : NetworkBehaviour
{
    [SerializeField] Renderer _ballRenderer;

    public void SetBallColor(Color newColor)
    {
        _ballRenderer.material.color = newColor;
    }
}
