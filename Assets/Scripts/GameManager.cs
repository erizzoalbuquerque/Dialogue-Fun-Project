using UnityEngine;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;

    [SerializeField] InputService _inputService;
    [SerializeField] DialogueRunner _dialogueRunner;
    
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }   

    void Awake()
    {
        if (_inputService == null)
        {
            Debug.LogError("InputService reference is not set in the GameManager. Please assign it in the inspector.");
        }

        if (_dialogueRunner == null)
        {
            Debug.LogError("DialogueRunner reference is not set in the GameManager. Please assign it in the inspector.");
        }  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
            _dialogueRunner.onDialogueStart.AddListener(OnDialogueStart);
            _dialogueRunner.onDialogueComplete.AddListener(OnDialogueComplete);        
    }

    void OnDisable()
    {
            _dialogueRunner.onDialogueStart.RemoveListener(OnDialogueStart);
            _dialogueRunner.onDialogueComplete.RemoveListener(OnDialogueComplete);        
    }

    void OnDialogueStart()
    {
        _inputService.EnablePlayerInput(false);
    }

    void OnDialogueComplete()
    {
        _inputService.EnablePlayerInput(true);
    }
}
