using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace DialogueSystem
{
    /// <summary>
    /// Manages dialogue characters and provides a centralized interface for starting dialogues and retrieving character information. This component should be added to a GameObject in the scene and will automatically register any DialogueCharacter components it finds. It also provides a reference to the DialogueRunner for convenience, but it's recommended to use the DialogueManager's methods instead of directly accessing the DialogueRunner when possible.
    /// </summary>
    [RequireComponent(typeof(DialogueRunner))]
    public class DialogueManager : MonoBehaviour
    {
        static DialogueManager _instance;
    
        private DialogueRunner _dialogueRunner;
        private List<DialogueCharacter> _registeredCharacters = new List<DialogueCharacter>();

        public static DialogueManager Instance => _instance;
        
        /// <summary>
        /// Gets the DialogueRunner instance for convenience. Avoid using it directly if you can.
        /// </summary>
        public DialogueRunner DialogueRunner => _dialogueRunner;


        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Debug.LogWarning("Multiple instances of DialogueManager detected. Destroying the new one.");
                Destroy(gameObject);
                return;
            }
            _instance = this;

            _dialogueRunner = GetComponent<DialogueRunner>();

            if (_dialogueRunner == null)
            {
                Debug.LogError("No DialogueRunner found in the scene. Please add one to use the DialogueManager component.");
            } 

        }
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        public void RegisterCharacter(DialogueCharacter character)
        {
            if (_registeredCharacters.Contains(character))
            {
                Debug.LogWarning($"Character '{character.Name}' is already registered with the DialogueManager.");
                return;
            }
            _registeredCharacters.Add(character);
        }

        public void StartDialogue(string nodeName)
        {
            _dialogueRunner.StartDialogue(nodeName);
        }

        public bool CheckNodeExists(string nodeName)
        {
            return _dialogueRunner.Dialogue.NodeExists(nodeName);
        }

        public DialogueCharacter GetDialogueCharacter(string characterName)
        {
            return _registeredCharacters.Find(c => c.Name == characterName);
        }
    }
}
