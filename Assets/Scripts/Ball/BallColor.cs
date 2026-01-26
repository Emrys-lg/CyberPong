using UnityEngine;

public class BallColor : MonoBehaviour
{
    [SerializeField] Renderer _ballRenderer;

    public void SetBallColor(Color newColor)
    {
        _ballRenderer.material.color = newColor;
    }
}
