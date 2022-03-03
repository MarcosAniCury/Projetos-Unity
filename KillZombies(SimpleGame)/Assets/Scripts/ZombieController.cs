using UnityEngine;

public class ZombieController : MonoBehaviour, IDeadly
{
    //Public vars
    public int DamageCaused = 30;
    public AudioClip ZombieDieSound;

    //Private vars
    GameObject player;
    float contWander;
    Vector3 randomPositionWander;
    Vector3 direction;

    //Components
    PlayerController playerController;
    MovementCharacter myMovement;
    AnimationCharacter myAnimation;
    Status myStatus;

    //CONSTs
    const double DISTANCE_TO_ZOMBIE_CHASE = 2.5;
    const double DISTANCE_TO_ZOMBIE_WANDER = 15;
    const float TIME_BETWEEN_WANDER_AGAIN = 4;
    const double ERROR_RATE_DISTANCE_ZOMBIE = 0.05;
    const int RADIO_TO_GENERATE_RANDOM_POSITION = 8;

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

        myMovement.Rotation(direction);
        myAnimation.Walk(direction.magnitude);

        bool attacking = true;
        if (distanceBetweenZombiAndPlayer > DISTANCE_TO_ZOMBIE_WANDER) {
            Wander();
            attacking = false;
        } else if (distanceBetweenZombiAndPlayer > DISTANCE_TO_ZOMBIE_CHASE) {
            direction = player.transform.position - transform.position;
            myMovement.Movement(direction.normalized, myStatus.Speed);
            attacking = false;
        } 
        
        myAnimation.Attack(attacking);
    }

    void Wander()
    {
        contWander -= Time.deltaTime;

        if (contWander <= 0) {
            randomPositionWander = GenerateRandomPosition();
            contWander += TIME_BETWEEN_WANDER_AGAIN;
        }

        bool isProxDistance = 
            Vector3.Distance(transform.position, randomPositionWander) <=
            ERROR_RATE_DISTANCE_ZOMBIE;

        if (!isProxDistance) {
            direction = randomPositionWander - transform.position;
            myMovement.Movement(direction.normalized, myStatus.Speed);
        }
    }

    Vector3 GenerateRandomPosition()
    {
        Vector3 position = Random.insideUnitSphere * RADIO_TO_GENERATE_RANDOM_POSITION;
        position += transform.position;
        position.y = transform.position.y;

        return position;
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
