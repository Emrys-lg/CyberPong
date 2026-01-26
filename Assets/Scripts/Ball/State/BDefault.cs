using UnityEngine;

public class BDefault : BallState
{
    [SerializeField] float decreaseEachFrame = 0.08f;
    Vector3 newVelocity;
    public override void OnEnter()
    {
        Debug.Log("EnterDefaultState");
        BallMain.Instance.BallColor.SetBallColor(Color.blue);
        BallMain.Instance.IsPowered = false;
    }
    public override void Do()
    {
    }

    public override void FixedDo() {

        if (BallMain.Instance.Rb.linearVelocity.x < 0 || BallMain.Instance.Rb.linearVelocity.z < 0) return;
        newVelocity = BallMain.Instance.Rb.linearVelocity - new Vector3(decreaseEachFrame, 0, decreaseEachFrame);
        BallMain.Instance.Rb.linearVelocity = newVelocity;
    }

    public override void OnExit()
    {
    }

}
