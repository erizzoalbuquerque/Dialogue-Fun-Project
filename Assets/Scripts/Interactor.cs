using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float _overlapRadius = 2f;
    [SerializeField] private Vector3 _localOffset = Vector3.forward;
    [SerializeField] private Transform _ignoreRoot;
    [SerializeField] private GameObject _interactionIndicator;

    private List<IInteractable> _interactables = new List<IInteractable>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _interactables.Clear();

        //Overlap a sphere in front of the player to check for interactables
        Collider[] hitColliders = Physics.OverlapSphere(transform.TransformPoint(_localOffset), _overlapRadius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            IInteractable interactable = hitColliders[i].GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (_ignoreRoot != null && hitColliders[i].transform.IsChildOf(_ignoreRoot))
                {
                    continue;
                }
                else
                {
                    _interactables.Add(interactable);
                }
            }
        }

        StartInteractionIndicator(_interactables.Count > 0);
    }

    public void Interact()
    {
        if (_interactables.Count > 0)
        {
            _interactables[0].Interact();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (_interactables.Count > 0)
        {
            Gizmos.color = Color.blue;
        }
        else
        {
            Gizmos.color = Color.yellow;
        }
        Gizmos.DrawWireSphere(transform.TransformPoint(_localOffset), _overlapRadius);
    }

    void StartInteractionIndicator(bool show)
    {
        if (_interactionIndicator != null)
        {
            _interactionIndicator.SetActive(show);
        }
    }
}
