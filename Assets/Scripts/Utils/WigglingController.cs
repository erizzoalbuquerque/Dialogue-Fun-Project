using UnityEngine;

public class WigglingController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Collider2D _stopCollider2D;

    [Header("Settings")]
    [SerializeField] float _positionAmplitude = 0.05f;
    [SerializeField] float _positionFrequency = 1f;
    [SerializeField, Range(0f,1f)] float _positionOffset = 0.25f;
    [SerializeField] float _rotationAmplitude = 2f;
    [SerializeField] float _rotationFrequency = 1f;
    [SerializeField, Range(0f,1f)] float _rotationOffset = 0f;
    [SerializeField] float _blendAnimationTime = 0.25f;
    [SerializeField] bool _applyRandomOffset = true;
    [SerializeField] bool _useLocalPositionSpace = false;

    private Vector3 _startPosition;
    private Vector3 _startLocalPosition;
    private Vector3 _startRotation;
    private float _randomOffset;
    private float _blendValue;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        _startLocalPosition = transform.localPosition;
        _startRotation = transform.rotation.eulerAngles;

        _randomOffset = Random.Range(0f, 1f); 

        if (_stopCollider2D != null)
        {
            _blendValue = _stopCollider2D.IsTouchingLayers(LayerMask.GetMask("Player")) ? 0f : 1f;
        }
        else
        {
            _blendValue = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime == 0f)
            return;

        float _blendTarget = 1f; // 0 = stop, 1 = full animation
        if (_stopCollider2D != null)
        {
            _blendTarget = _stopCollider2D.IsTouchingLayers(LayerMask.GetMask("Player")) ? 0f : 1f;
        }

        _blendValue = Mathf.MoveTowards(_blendValue, _blendTarget, Time.deltaTime / _blendAnimationTime);

        float positionDelta = CalculateOscillation(_positionAmplitude, _positionFrequency, _positionOffset + (_applyRandomOffset? _randomOffset : 0f));
        float rotationDelta = CalculateOscillation(_rotationAmplitude, _rotationFrequency, _rotationOffset + (_applyRandomOffset? _randomOffset : 0f));

        if (_useLocalPositionSpace)
        {
            transform.localPosition = _startLocalPosition + Vector3.up * positionDelta * _blendValue;
        }
        else
        {
            transform.position = _startPosition + Vector3.up * positionDelta * _blendValue;
        }
        transform.rotation = Quaternion.Euler(_startRotation.x, _startRotation.y, _startRotation.z + rotationDelta * _blendValue);        
    }

    float CalculateOscillation(float amplitude, float frequency, float offset)
    {
        return amplitude * Mathf.Sin((Time.time * frequency + offset) * 2f * Mathf.PI);
    }
}
