using UnityEngine;
using UnityEngine.Events;

public class GameObjectTrigger : MonoBehaviour
{
    public UnityEvent Ev_OnAwake;
    public UnityEvent Ev_OnEnable;
    
    public void Awake()
    {
        Ev_OnAwake.Invoke();
    }

    protected void OnEnable()
    {
        Ev_OnEnable.Invoke();
    }
}
