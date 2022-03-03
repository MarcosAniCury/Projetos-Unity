using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Public vars
    public LayerMask FlorMask;
    public GameObject GameOverComponent;
    public UIController UIController;
    public AudioClip DamageSound;
    
    //Private vars
    private Vector3 direction;

    //CONSTs
    const int RAY_LENGTH = 100;
    const string INPUT_MOUSE_LEFT = "Fire1";
    const int GAME_RESUME = 1;
    const int GAME_PAUSE = 0;

    //Components
    MovementCharacter myMovement;
    AnimationCharacter myAnimator;
    [HideInInspector]
    public Status myStatus;

    void Start() 
    {
        Time.timeScale = GAME_RESUME;
        myMovement = GetComponent<MovementCharacter>();
        myAnimator = GetComponent<AnimationCharacter>();
        myStatus = GetComponent<Status>();
    }
    
    void Update()
    {
        //Use to walk with awsd
        float axisX = Input.GetAxis("Horizontal");
        float axisZ = Input.GetAxis("Vertical");

        direction = new Vector3(axisX, 0, axisZ);

        myAnimator.Walk(direction.magnitude);

        if (myStatus.Life <= 0 && Input.GetButtonDown(INPUT_MOUSE_LEFT)) {
            SceneManager.LoadScene("MainScene");
        }
    }

    void FixedUpdate() 
    {
        myMovement.Movement(direction, myStatus.Speed);

        PlayerMovement();
    }

    public void takeDamage(int damageTaked) 
    {
        myStatus.Life -= damageTaked;
        UIController.updatedLivePlayerSlider();
        SoundController.instance.PlayOneShot(DamageSound);
        if (myStatus.Life <= 0) {
            Time.timeScale = GAME_PAUSE;
            GameOverComponent.SetActive(true);
        }
    }

    void PlayerMovement()
    {
        Ray rayCamera = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit rayCameraImpact;

        if (Physics.Raycast(rayCamera,out rayCameraImpact, RAY_LENGTH, FlorMask)) {
            Vector3 playerLookPosition = rayCameraImpact.point - transform.position;
            playerLookPosition.y = transform.position.y;

            myMovement.Rotation(playerLookPosition);
        }
    }
}
