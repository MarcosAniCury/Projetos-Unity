using UnityEngine;

public class ZombieController : MonoBehaviour, IDeadly
{
    //Public vars
    public int DamageCaused = 30;
    public AudioClip ZombieDieSound;

    //Private vars
    private GameObject player;

    //Components
    PlayerController playerController;
    MovementCharacter myMovement;
    AnimationCharacter myAnimation;
    Status myStatus;


    void Start() 
    {
        player = GameObject.FindWithTag(Constants.TAG_PLAYER);
        playerController = player.GetComponent<PlayerController>(); 
        myMovement = GetComponent<MovementCharacter>(); 
        myAnimation = GetComponent<AnimationCharacter>();
        myStatus = GetComponent<Status>();

        SetZombieRandom();
    }

    void FixedUpdate()
    {
        float distanceBetweenZombiAndPlayer = Vector3.Distance(
            transform.position, player.transform.position
        );

        Vector3 direction = player.transform.position - transform.position;
        myMovement.Rotation(direction);

        bool attacking = true;

        if (distanceBetweenZombiAndPlayer > 2.5) {
            myMovement.Movement(direction.normalized, myStatus.Speed);
            attacking = false;
        } 
        
        myAnimation.Attack(attacking);
    }

    void AttackPlayer() 
    {
        playerController.TakeDamage(DamageCaused);
    }

    void SetZombieRandom()
    {
        int generateTypeZombie = Random.Range(1, 28);
        transform.GetChild(generateTypeZombie).gameObject.SetActive(true);
    }

    public void TakeDamage(int damage)
    {
        myStatus.Life -= damage;
        if (myStatus.Life <= 0) {
            Dead();
        }
    }

    public void Dead()
    {
        Destroy(gameObject);
        SoundController.instance.PlayOneShot(ZombieDieSound);
    }
}
