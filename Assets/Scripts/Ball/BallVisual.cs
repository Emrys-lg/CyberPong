using Unity.Netcode;
using UnityEngine;

public class BallVisual : NetworkBehaviour
{
    [SerializeField] Renderer _ballRenderer;


    [SerializeField] Gradient _frontGradient;
    [SerializeField] Gradient _backGradient;

    private void Update()
    {
        SetBallColorClientRpc(_frontGradient.Evaluate(BallMain.Instance.BallMove.AverageCurrentVelocity/ BallMain.Instance.BallMove.MaxMoveSpeed), _backGradient.Evaluate(BallMain.Instance.BallMove.AverageCurrentVelocity / BallMain.Instance.BallMove.MaxMoveSpeed));
    }

    [ClientRpc]
    public void SetBallColorClientRpc(Color frontColor, Color backColor)
    {
        _ballRenderer.material.SetColor("_FrontColor", frontColor);
        _ballRenderer.material.SetColor("_BackColor", backColor);
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


}
