using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementTimePerSecond = 10;
    Vector3 direction;
    
    void Update()
    {
        //Use to walk with awsd
        float axisX = Input.GetAxis("Horizontal");
        float axisZ = Input.GetAxis("Vertical");

        direction = new Vector3(axisX, 0, axisZ);

        //Way to move the object "personagens" using the object
        //transform.Translate(direction * Time.deltaTime * movementTimePerSecond);

        bool running = false;
        if (direction != Vector3.zero) {
            running = true;
        }

        GetComponent<Animator>().SetBool("Running", running);
    }

    void FixedUpdate() 
    {
        //Way to move the object "personagens" using physics
        GetComponent<Rigidbody>().MovePosition(
            GetComponent<Rigidbody>().position + (
                direction * Time.deltaTime * movementTimePerSecond
                )
            );
    }
}
