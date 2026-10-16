using UnityEngine;

public class CharacterVisual : MonoBehaviour
{
    [SerializeField] CharacterController2d _characterController2D;
    [SerializeField] Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("IsMoving", _characterController2D.IsMoving);
    }
}
