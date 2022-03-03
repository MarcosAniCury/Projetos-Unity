using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IDeadly
{
    //Public vars
    public LayerMask FlorMask;
    public GameObject GameOverComponent;
    public UIController UIController;
    public AudioClip DamageSound;
    
    //Private vars
    private Vector3 direction;

    //Components
    MovementCharacter myMovement;
    AnimationCharacter myAnimator;
    [HideInInspector]
    public Status myStatus;

    void Start() 
    {
        Time.timeScale = Constants.GAME_RESUME;
        myMovement = GetComponent<MovementCharacter>();
        myAnimator = GetComponent<AnimationCharacter>();
        myStatus = GetComponent<Status>();
    }
    
    void Update()
    {
        //Use to walk with awsd
        float axisX = Input.GetAxisRaw("Horizontal");
        float axisZ = Input.GetAxisRaw("Vertical");

        direction = new Vector3(axisX, 0, axisZ);

        myAnimator.Walk(direction.magnitude);

        if (myStatus.Life <= 0 && Input.GetButtonDown(Constants.INPUT_MOUSE_LEFT)) {
            SceneManager.LoadScene("MainScene");
        }
    }

    void FixedUpdate() 
    {
        myMovement.Movement(direction, myStatus.Speed);

        PlayerMovement();
    }

    void PlayerMovement()
    {
        Ray rayCamera = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit rayCameraImpact;

        if (Physics.Raycast(rayCamera,out rayCameraImpact, Constants.RAY_LENGTH, FlorMask)) {
            Vector3 playerLookPosition = rayCameraImpact.point - transform.position;
            playerLookPosition.y = transform.position.y;

            myMovement.Rotation(playerLookPosition);
        }
    }

    public void TakeDamage(int damageTaked) 
    {
        myStatus.Life -= damageTaked;
        UIController.updatedLivePlayerSlider();
        SoundController.instance.PlayOneShot(DamageSound);
        if (myStatus.Life <= 0) {
            Dead();
        }
    }

    public void Dead()
    {
        Time.timeScale = Constants.GAME_PAUSE;
        GameOverComponent.SetActive(true);
    }
}
