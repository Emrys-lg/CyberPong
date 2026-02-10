  using Unity.Netcode;
using UnityEngine;

public class BallVisuals : NetworkBehaviour
{
    [SerializeField] private Renderer _ballRenderer;

    private NetworkVariable<float> _colorTransition = new NetworkVariable<float>(0f);
    private NetworkVariable<float> _vertexAmount = new NetworkVariable<float>(0f);
    private NetworkVariable<float> _vertexFrequency = new NetworkVariable<float>(0f);

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        _colorTransition.OnValueChanged += OnColorTransitionChanged;
        _vertexAmount.OnValueChanged += OnVertexAmountChanged;
        _vertexFrequency.OnValueChanged += OnVertexFrequencyChanged;
    }

    private void Update()
    {
        if (!IsServer) return;

        float velocityRatio = BallMain.Instance.BallMove.AverageCurrentVelocity / BallMain.Instance.BallMove.MaxMoveSpeed;

        _colorTransition.Value = velocityRatio;
        _vertexAmount.Value = BallMain.Instance.BallMove.AverageCurrentVelocity / 20f;
        _vertexFrequency.Value = BallMain.Instance.BallMove.AverageCurrentVelocity / 5f;
    }

    private void OnColorTransitionChanged(float oldValue, float newValue)
    {
        _ballRenderer.material.SetFloat("_ColorTransition", newValue);
    }

    private void OnVertexAmountChanged(float oldValue, float newValue)
    {
        _ballRenderer.material.SetVector("_VertexAmount", new Vector3(newValue, newValue, newValue));
    }

    private void OnVertexFrequencyChanged(float oldValue, float newValue)
    {
        _ballRenderer.material.SetFloat("_VertexFrequency", newValue);
    }

    [ClientRpc]
    public void SetBallFresnelColorClientRpc(Color color)
    {
        _ballRenderer.material.SetColor("_FresnelColor", color);
    }
}