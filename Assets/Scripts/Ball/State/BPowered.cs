using UnityEngine;

public class BPowered : BallState
{
    [SerializeField] float _timeUntilSwitch = 5;
    [SerializeField] float _timeOnEnter;

    [SerializeField] float _linearDampingPowered = 0f;
    [SerializeField] float _angularDampingPowered = 0f;

    public override void OnEnter()
    {
        BallMain.Instance.Rb.linearDamping = _linearDampingPowered;
        BallMain.Instance.Rb.angularDamping = _angularDampingPowered;
        BallMain.Instance.BallVisual.SetBallFresnelColorClientRpc(Color.purple);


        _timeOnEnter = Time.time;
        BallMain.Instance.IsPowered = true;
        BallMain.Instance.BallVisual._colorTransition.Value = 1f;
    }

    public override void Do()
    {
        if(Time.time >= _timeOnEnter + _timeUntilSwitch)
        {
            BallMain.Instance.BallStateBrain.SwitchBallState(BallMain.Instance.BallStateBrain._ballDefaultState);
        }
    }
}
