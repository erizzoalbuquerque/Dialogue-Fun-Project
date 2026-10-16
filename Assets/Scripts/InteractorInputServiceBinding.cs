using UnityEngine;

public class InteractorInputServiceBinding : MonoBehaviour
{
    [SerializeField] private Interactor _interactor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        InputService _inputService = GameManager.Instance.InputService;
        if (_inputService != null)
        {
            _inputService.playerActionMapEnabledChanged += OnPlayerActionMapEnabledChanged;
        }
    }

    void OnDisable()
    {
        InputService inputService = GameManager.Instance.InputService;
        if (inputService != null)
        {
            inputService.playerActionMapEnabledChanged -= OnPlayerActionMapEnabledChanged;
        }
    }

    private void OnPlayerActionMapEnabledChanged(bool isEnabled)
    {
        if (_interactor != null)
        {
            _interactor.enabled = isEnabled;
        }
    }
}
