using UnityEngine;

public class BDefault : BallState
{
    public override void OnEnter()
    {
        Debug.Log("EnterDefaultState");
        BallMain.Instance.BallColor.SetBallColorClientRpc(Color.blue);
        BallMain.Instance.IsPowered = false;
    }
    public override void Do()
    {
    }

    public override void FixedDo() {

    }

    public override void OnExit()
    {
    }

}
