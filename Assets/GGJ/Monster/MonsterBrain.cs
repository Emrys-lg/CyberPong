using UnityEngine;
using System.Collections.Generic;

public class MonsterBrain : MonoBehaviour
{
    [SerializeField] MaskState currentMask;
    [SerializeField] List<MaskState> MaskList;

    public void SwitchMask()
    {
        currentMask.OnExit();
        currentMask = GetRandomMask();
        currentMask.OnEnter();
    }

    public MaskState GetRandomMask()
    {
        int randomIndex = Random.Range(0, MaskList.Count);
        return MaskList[randomIndex];
    }
}
