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

    }

    public override void OnExit()
    {
    }

}
