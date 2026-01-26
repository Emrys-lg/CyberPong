using UnityEngine;

public class BPowered : BallState
{
    [SerializeField] float _timeUntilSwitch = 5;
    public override void OnEnter()
    {
        BallMain.Instance.BallColor.SetBallColor(Color.red);
        BallMain.Instance.IsPowered = true;
    }

    public override void FixedDo()
    {
        if(startTime == _timeUntilSwitch)
        {
            BallMain.Instance.BallStateBrain.SwitchBallState(BallMain.Instance.BallStateBrain._ballDefaultState);
        }
    }
}
