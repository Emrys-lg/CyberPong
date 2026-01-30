using UnityEngine;

public class MonsterTime : MonoBehaviour
{
    public float DefaultTimeUntilAction = 0;
    [SerializeField] private float _currentTimeUntilNextAction;

    private void Start()
    {
        _currentTimeUntilNextAction = DefaultTimeUntilAction;
    }
}
