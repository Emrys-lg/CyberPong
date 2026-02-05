using UnityEngine;

public class BDefault : BallState
{
    [SerializeField] float _linearDampingDefault = 0.5f;
    [SerializeField] float _angularDampingDefault = 0.5f;


    public override void OnEnter()
    {
        BallMain.Instance.Rb.linearDamping = _linearDampingDefault;
        BallMain.Instance.Rb.angularDamping = _angularDampingDefault;
        BallMain.Instance.IsPowered = false;

        BallMain.Instance.BallVisual.SetBallFresnelColorClientRpc(Color.white);
    }


}
