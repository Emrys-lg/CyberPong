using UnityEngine;

public class BDefault : BallState
{
    public override void OnEnter()
    {
        BallMain.Instance.BallColor.SetBallColor(Color.blue);
        BallMain.Instance.IsPowered = false;
    }
    public override void Do()
    {
    }

    public override void FixedDo()
    {
    }

    public override void OnExit()
    {
    }

}
