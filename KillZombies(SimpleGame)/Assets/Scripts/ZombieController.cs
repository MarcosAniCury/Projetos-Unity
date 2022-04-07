using UnityEngine;

public class ZombieController : MonoBehaviour, IDeadly
{
    //Public vars
    public int DamageCaused = 30;
    public AudioClip ZombieDieSound;
    public GameObject MedKitPrefab;
    
    [HideInInspector]
    public EnemyGenerate MyGenerate;

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
    UIController gameInterface;

    //CONSTs
    const double DISTANCE_TO_ZOMBIE_CHASE = 2.5;
    const double DISTANCE_TO_ZOMBIE_WANDER = 15;
    const float TIME_BETWEEN_WANDER_AGAIN = 4;
    const float ERROR_RATE_DISTANCE_ZOMBIE = 0.05f;
    const int RADIO_TO_GENERATE_RANDOM_POSITION = 10;
    const float CHANCE_TO_GENERATE_MED_KIT = 0.1f;

    void Start() 
    {
        player = GameObject.FindWithTag(Constants.TAG_PLAYER);
        playerController = player.GetComponent<PlayerController>(); 
        myMovement = GetComponent<MovementCharacter>(); 
        myAnimation = GetComponent<AnimationCharacter>();
        myStatus = GetComponent<Status>();
        gameInterface = GameObject.FindObjectOfType(typeof(UIController)) as UIController;

        SetZombieRandom();
    }

    void FixedUpdate()
    {
        float distanceBetweenZombiAndPlayer = Vector3.Distance(
            transform.position, player.transform.position
        );

        bool attacking = true;
        if (distanceBetweenZombiAndPlayer > DISTANCE_TO_ZOMBIE_WANDER) {
            Wander();
            attacking = false;
        } else if (distanceBetweenZombiAndPlayer > DISTANCE_TO_ZOMBIE_CHASE) {
            direction = player.transform.position - transform.position;
            myMovement.Rotation(direction);
            myMovement.Movement(direction.normalized, myStatus.Speed);
            attacking = false;
        } 

        myAnimation.Walk(direction.magnitude);
        myAnimation.Attack(attacking);
    }

    void Wander()
    {
        contWander -= Time.deltaTime;

        if (contWander <= 0) {
            randomPositionWander = GenerateRandomPosition();
            contWander += TIME_BETWEEN_WANDER_AGAIN;
        }

        float speedMovement = 0;

        bool isProxDistance = 
            Vector3.Distance(transform.position, randomPositionWander) <=
            ERROR_RATE_DISTANCE_ZOMBIE;

        if (!isProxDistance) {
            direction = randomPositionWander - transform.position;
            myMovement.Rotation(direction);
            speedMovement = myStatus.Speed;
        }

        myMovement.Movement(direction, speedMovement);
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

        GenerateMedKit();

        gameInterface.AddOneZombieDead();
        
        MyGenerate.DecreseNumberOfZombieAlive();
        
        SoundController.instance.PlayOneShot(ZombieDieSound);
    }

    void GenerateMedKit()
    {   
        if(Random.value <= CHANCE_TO_GENERATE_MED_KIT) {
            Instantiate(MedKitPrefab, transform.position, Quaternion.identity);
        }
    }
}
