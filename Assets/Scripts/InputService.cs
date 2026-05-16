using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : MonoBehaviour
{    
    [SerializeField] private InputActionAsset _inputActionAsset;

    InputActionMap _playerActionMap;
    InputActionMap _uiActionMap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerActionMap = _inputActionAsset.FindActionMap("Player");
        _uiActionMap = _inputActionAsset.FindActionMap("UI");        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnablePlayerInput(bool enable)
    {
        if (enable)
        {
            _playerActionMap.Enable();
        }
        else
        {
            _playerActionMap.Disable();
        }   
        print($"Player input {(enable ? "enabled" : "disabled")}");
    }
}
