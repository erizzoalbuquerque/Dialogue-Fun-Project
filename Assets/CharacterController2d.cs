using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2d : MonoBehaviour, IMovable
{
    [SerializeField] private float _moveSpeed = 5f;

    public bool IsMoving => _rb.linearVelocity.magnitude >= 0.01f;

    Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
        _rb.freezeRotation = true;
    }

    public void Move(Vector3 direction)
    {
        _rb.linearVelocity = direction.normalized * _moveSpeed;
    }
}
