using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pos3List;
    [SerializeField] private List<GameObject> _pos2List;
    [SerializeField] private List<GameObject> _pos1List;

    [SerializeField] private GameObject _currentPos;

    public void MonsterMove()
    {
        GetNextRandomPos(GetNextPosList());
    }

    private List<GameObject> GetNextPosList()
    {
        return _pos3List;
    }

    private GameObject GetNextRandomPos(List<GameObject> posList)
    {
        int randomIndex = Random.Range(0, posList.Count);
        return posList[randomIndex];
    }

}
