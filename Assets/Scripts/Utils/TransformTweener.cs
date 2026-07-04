using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class TransformTweener : MonoBehaviour
{
    [SerializeField] bool _playOnEnable = true;
    [SerializeField] AnimationType _animationType;
    [SerializeField] Ease _ease = Ease.Unset;
    [SerializeField] Vector3 _startValue = Vector3.one;
    [SerializeField] float _duration = 0.5f;
    [SerializeField] float _punchMultiplier = 1.1f;

    Transform _target;
    Tween _currentTween;

    enum AnimationType
    {
        Move,
        Rotate,
        Scale,
        PunchPostion,
        PunchRotation,
        PunchScale,
        //ShakePosition,
        //ShakeRotation,
        //ShakeScale
    }

    const int PUNCH_VIBRATO = 3;
    const float PUNCH_ELASTICITY = 0.3f;


    // Start is called before the first frame update
    void Awake()
    {
        _currentTween = null;
        _target = this.gameObject.transform;
    }

    private void OnEnable()
    {
        if (_playOnEnable)
        {
            Animate();
        }
    }

    private void OnDisable()
    {
        KillCurrentTween();
    }

    private void OnDestroy()
    {
        KillCurrentTween();
    }

    public void Animate()
    {
        KillCurrentTween();

        switch (_animationType)
        {
            case AnimationType.Move:
                _currentTween = _target.DOLocalMove(_startValue, _duration).From();
                break;
            case AnimationType.Rotate:
                _currentTween = _target.DOLocalRotate(_startValue, _duration).From();
                break;
            case AnimationType.Scale:
                _currentTween = _target.DOScale(_startValue, _duration).From();
                break;
            case AnimationType.PunchPostion:
                _currentTween = _target.DOPunchPosition(Vector3.up * _punchMultiplier, _duration, PUNCH_VIBRATO, PUNCH_ELASTICITY);
                break;
            case AnimationType.PunchRotation:
                _currentTween = _target.DOPunchRotation(Vector3.forward * _punchMultiplier, _duration, PUNCH_VIBRATO, PUNCH_ELASTICITY);
                break;
            case AnimationType.PunchScale:
                _currentTween = _target.DOPunchScale(_target.localScale * (_punchMultiplier - 1f), _duration, PUNCH_VIBRATO, PUNCH_ELASTICITY);
                break;
        }

        _currentTween = _currentTween.SetEase(_ease);
    }

    void KillCurrentTween()
    {
        if (_currentTween == null) return;

        _currentTween.Complete();
        _currentTween.Kill();
        _currentTween = null;
    }
}
