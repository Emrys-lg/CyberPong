using Unity.Netcode;
using UnityEngine;

public class BallStateBrain : NetworkBehaviour
{
    public BDefault _ballDefaultState;
    public BPowered _ballPoweredState;

    public BallState CurrentBallState;

    private void Start()
    {
        CurrentBallState = _ballDefaultState;
    }

    public void SwitchBallState(BallState newBallState)
    {
        CurrentBallState.OnExit();
        CurrentBallState = newBallState;
        CurrentBallState.OnEnter();
    }

    private void Update()
    {
        CurrentBallState.Do();
    }

    private void FixedUpdate()
    {
        CurrentBallState.FixedDo();
    }
}
