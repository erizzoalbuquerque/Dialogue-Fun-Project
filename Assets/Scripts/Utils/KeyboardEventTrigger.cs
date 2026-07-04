using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class KeyboardEventTrigger : MonoBehaviour
{
    [SerializeField] Key _key;
    [SerializeField] UnityEvent _onKeyPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check if _key was pressed
        if (Keyboard.current[_key].wasPressedThisFrame)
        {
            _onKeyPressed.Invoke();
        }
    }
}
