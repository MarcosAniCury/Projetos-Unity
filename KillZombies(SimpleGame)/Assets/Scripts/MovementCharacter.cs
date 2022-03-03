using UnityEngine;

public class MovementCharacter : MonoBehaviour
{

    //Components
    Rigidbody myRigidbody;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    public void Movement(Vector3 direction, float speedMovement)
    {
        //Way to move the character using physics
        myRigidbody.velocity = direction * speedMovement;
    }

    public void Rotation(Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction);
        myRigidbody.MoveRotation(rotation);
    }
}
