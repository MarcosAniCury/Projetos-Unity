using UnityEngine;

public class PlayerController : MonoBehaviour, IDeadly, ICurable
{
    //Public vars
    public LayerMask FlorMask;
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

    public void Healing(int amountHeal)
    {
        myStatus.Life += amountHeal;
        if (myStatus.Life > myStatus.InitialLife) {
            myStatus.Life = myStatus.InitialLife;
        }

        UIController.updatedLivePlayerSlider();
    }

    public void Dead()
    {
        UIController.GameOver();
    }
}
