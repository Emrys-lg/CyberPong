using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class BallVisual : NetworkBehaviour
{
    [SerializeField] Renderer _ballRenderer;

    private void Update()
    {
        SetBallColorTransitionClientRpc(BallMain.Instance.BallMove.AverageCurrentVelocity/ BallMain.Instance.BallMove.MaxMoveSpeed);
        SetBallVertexAmountClientRpc(BallMain.Instance.BallMove.AverageCurrentVelocity, 20);
        SetBallVertexFrequencyClientRpc(BallMain.Instance.BallMove.AverageCurrentVelocity, 5);

    }

    [ClientRpc]
    public void SetBallColorTransitionClientRpc(float value)
    {
        _ballRenderer.material.SetFloat("_ColorTransition", value);
    }
    [ClientRpc]
    public void SetBallVertexAmountClientRpc(float amount, float divider)
    {
        amount = amount / divider;
        _ballRenderer.material.SetVector("_VertexAmount", new Vector3(amount, amount, amount));
    }
    [ClientRpc]
    public void SetBallVertexFrequencyClientRpc(float frequency, float divider)
    {
        _ballRenderer.material.SetFloat("_VertexFrequency", frequency/divider);
    }

    [ClientRpc]
    public void SetBallFresnelColorClientRpc(Color color)
    {
        _ballRenderer.material.SetColor("_FresnelColor", color);
        Debug.Log("Hello");
    }


}
