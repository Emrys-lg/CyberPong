using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwing : MonoBehaviour
{
    [SerializeField]  PlayerMain _playerMain;
    [SerializeField] GameObject _bat;

    private void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            _bat.SetActive(true);
        }
        else
        {
            _bat.SetActive(false);
        }
    }



}
