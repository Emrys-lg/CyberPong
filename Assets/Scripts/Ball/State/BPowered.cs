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


        _timeOnEnter = Time.time;
        BallMain.Instance.IsPowered = true;
    }

    public override void Do()
    {
        if(Time.time >= _timeOnEnter + _timeUntilSwitch)
        {
            BallMain.Instance.BallStateBrain.SwitchBallState(BallMain.Instance.BallStateBrain._ballDefaultState);
        }
    }
}
