using UnityEngine;

public class MonsterMain : MonoBehaviour
{
    private static MonsterMain instance = null;
    public static MonsterMain Instance => instance;

    public MonsterBrain MonsterBrain;
    public MonsterBrain MonsterTime;
    public MonsterMovement MonsterMovement;

    public enum MaskType {Red, Green, Blue }

    public MaskType CurrentMask;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }



}
