using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //public vars
    public float movementTimePerSecond = 10;
    public LayerMask florMask;
    public GameObject GameOverCanvas;
    public bool gameOver = false;

    //private vars
    Vector3 direction;

    //CONSTs
    const string ANIMATOR_RUNNING = "Running";

    const int RAY_LENGTH = 100;
    const string INPUT_MOUSE_LEFT = "Fire1";
    const int GAME_RESUME = 1;

    void Start() 
    {
        Time.timeScale = GAME_RESUME;
    }
    
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

        if (gameOver && Input.GetButtonDown(INPUT_MOUSE_LEFT)) {
            SceneManager.LoadScene("MainScene");
        }
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
