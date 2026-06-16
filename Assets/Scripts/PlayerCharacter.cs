using UnityEngine;

public class PlayerCharacter : MonoBehaviour, IMovable
{
    [SerializeField] CharacterController characterController;
    [SerializeField] private float moveSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector3 direction)
    {
        Vector3 moveVelocity = direction * moveSpeed;
        characterController.Move(moveVelocity * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            this.transform.rotation = Quaternion.LookRotation(direction);
        }   
    }
}
