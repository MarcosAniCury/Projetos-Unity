using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementTimePerSecond = 10;
    Vector3 direction;
    public LayerMask florMask;

    const string ANIMATOR_RUNNING = "Running";

    const int RAY_LENGTH = 100;
    
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

        GetComponent<Animator>().SetBool(ANIMATOR_RUNNING, running);
    }

    void FixedUpdate() 
    {
        //Way to move the object "personagens" using physics
        GetComponent<Rigidbody>().MovePosition(
            GetComponent<Rigidbody>().position + (
                direction * Time.deltaTime * movementTimePerSecond
                )
            );

        Ray rayCamera = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit rayCameraImpact;

        if (Physics.Raycast(rayCamera,out rayCameraImpact, RAY_LENGTH, florMask)) {
            Vector3 playerLookPosition = rayCameraImpact.point - transform.position;

            playerLookPosition.y = transform.position.y;

            Quaternion playerLookRotation = Quaternion.LookRotation(playerLookPosition);

            GetComponent<Rigidbody>().MoveRotation(playerLookRotation);
        }
    }
}
