using UnityEngine;

public class BPowered : BallState
{
    [SerializeField] float _timeUntilSwitch = 5;
    [SerializeField] float _timeOnEnter;
    public override void OnEnter()
    {
        Debug.Log("EnterPoweredState");
        _timeOnEnter = Time.time;
        BallMain.Instance.BallColor.SetBallColor(Color.red);
        BallMain.Instance.IsPowered = true;
    }

    public override void FixedDo()
    {
        if(Time.time >= _timeOnEnter + _timeUntilSwitch)
        {
            BallMain.Instance.BallStateBrain.SwitchBallState(BallMain.Instance.BallStateBrain._ballDefaultState);
        }
    }
}
