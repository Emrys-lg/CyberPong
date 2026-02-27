  using Unity.Netcode;
using UnityEngine;

public class BallVisuals : NetworkBehaviour
{
    [SerializeField] private Renderer _ballRenderer;

    public NetworkVariable<float> _colorTransition = new NetworkVariable<float>(0f);
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
    }

    public void OnColorTransitionChanged(float oldValue, float newValue)
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