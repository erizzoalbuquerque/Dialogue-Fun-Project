using UnityEngine;
using Yarn.Unity;


namespace DialogueSystem
{
    /// <summary>
    /// A simple component that can be added to a GameObject to start a dialogue when the StartDialogue method is called. It requires a reference to the DialogueManager and the name of the dialogue node to start. This can be used for triggering dialogues from various events in the game, such as interacting with an NPC or entering a specific area.
    /// </summary>
    public class Dialogue : MonoBehaviour
    {
        [SerializeField] private string _dialogueNode;

        DialogueManager _dialogueManager;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _dialogueManager = DialogueManager.Instance;

            if (_dialogueManager == null)
            {
                Debug.LogError("No DialogueManager found in the scene. Please add one to use the Dialogue component.");
            }

            if (_dialogueManager != null && string.IsNullOrEmpty(_dialogueNode))
            {
                Debug.LogWarning("Dialogue node is not set. Please specify a dialogue node to start.");
            }

            if (_dialogueManager.CheckNodeExists(_dialogueNode) == false)
            {
                Debug.LogWarning($"Dialogue node '{_dialogueNode}' does not exist. Please check the node name.");
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void StartDialogue()
        {
            if (_dialogueManager != null)
            {
                _dialogueManager.StartDialogue(_dialogueNode);
            }
        }
    }
}
