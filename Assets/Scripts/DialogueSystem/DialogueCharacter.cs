using UnityEngine;

namespace DialogueSystem
{
    public class DialogueCharacter : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private CharacterVoiceSO _voice;
        [SerializeField] private Transform _bobblingPivot;
        public string Name => _name;
        public Transform BobblingPivot => _bobblingPivot;
        public CharacterVoiceSO Voice => _voice;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            DialogueManager.Instance.RegisterCharacter(this);        
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
