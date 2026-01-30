using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pos3List;
    [SerializeField] private List<GameObject> _pos2List;
    [SerializeField] private List<GameObject> _pos1List;

    [SerializeField] private GameObject _currentPos;

    public void MonsterMoveTowardPlayer()
    {
        transform.position = GetNextRandomPos(GetNextPosList());
    }

    public void MonsterFlee()
    {
        transform.position = GetRandom3Pos();
    }

    private List<GameObject> GetNextPosList()
    {
        return _pos3List; //ici a finir
    }

    private Vector3 GetNextRandomPos(List<GameObject> posList)
    {
        int randomIndex = Random.Range(0, posList.Count);
        return posList[randomIndex].transform.position;
    }

    private Vector3 GetRandom3Pos() {
        int randomIndex = Random.Range(0, _pos3List.Count);
        return _pos3List[randomIndex].transform.position;

    }

    

}
