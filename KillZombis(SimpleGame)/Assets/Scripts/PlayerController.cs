using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Public vars
    public float MovementTimePerSecond = 10;
    public LayerMask FlorMask;
    public GameObject GameOverComponent;
    public int Life = 100;
    public UIController UIController;
    
    //Private vars
    private Vector3 direction;

    //CONSTs
    const string ANIMATOR_RUNNING = "Running";

    const int RAY_LENGTH = 100;
    const string INPUT_MOUSE_LEFT = "Fire1";
    const int GAME_RESUME = 1;
    const int GAME_PAUSE = 0;

    //Components
    Rigidbody playerRigidbody;

    void Start() 
    {
        Time.timeScale = GAME_RESUME;
        playerRigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        //Use to walk with awsd
        float axisX = Input.GetAxis("Horizontal");
        float axisZ = Input.GetAxis("Vertical");

        direction = new Vector3(axisX, 0, axisZ);

        //Way to move the object "personagens" using the object
        //transform.Translate(direction * Time.deltaTime * movementTimePerSecond);

        bool running = direction != Vector3.zero;

        GetComponent<Animator>().SetBool(ANIMATOR_RUNNING, running);

        if (Life <= 0 && Input.GetButtonDown(INPUT_MOUSE_LEFT)) {
            SceneManager.LoadScene("MainScene");
        }
    }

    void FixedUpdate() 
    {
        //Way to move the object "personagens" using physics
        playerRigidbody.MovePosition(
            playerRigidbody.position + (
                direction * Time.deltaTime * MovementTimePerSecond
                )
            );

        Ray rayCamera = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit rayCameraImpact;

        if (Physics.Raycast(rayCamera,out rayCameraImpact, RAY_LENGTH, FlorMask)) {
            Vector3 playerLookPosition = rayCameraImpact.point - transform.position;

            playerLookPosition.y = transform.position.y;

            Quaternion playerLookRotation = Quaternion.LookRotation(playerLookPosition);

            playerRigidbody.MoveRotation(playerLookRotation);
        }
    }

    public void takeDamage(int damageTaked) 
    {
        Life -= damageTaked;
        UIController.updatedLivePlayerSlider();
        if (Life <= 0) {
            Time.timeScale = GAME_PAUSE;
            GameOverComponent.SetActive(true);
        }
    }
}
