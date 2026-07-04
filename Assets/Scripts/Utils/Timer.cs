using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] float _duration = 0.5f;
    [SerializeField, Range(0,1)] float _startOffset = 0f;    
    [SerializeField] bool _loop = false;
    [SerializeField] UnityEvent _timeOut;

    float _timer;

    public float CurrentTime { get => _timer; }
    public float Duration { get => _duration; }

    void Start()
    {
        _timer = _duration - _startOffset * _duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0f)
        {
            _timer -= Time.deltaTime;
            
            if (_timer <= 0f)
            {
                Finish();
            }
        }        
    }

    void OnDisable()
    {
        Reset();
    }

    void Reset()
    {
        // TODO: Reset should take in consideration the offset as in _timer = _duration - _startOffset * _duration
        _timer = _duration;
    }

    [ContextMenu("Finish")]
    void Finish()
    {
        _timer = 0f;
        _timeOut.Invoke();
        if (_loop)
        {
            Reset();
        }
    }
}
