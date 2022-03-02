using UnityEngine;

public class ZombieController : MonoBehaviour
{
    //Public vars
    public float ZombiSpeed = 5;

    public int DamageCaused = 30;

    //Private vars
    private GameObject player;

    //CONSTs
    const string ANIMATOR_ATTACKING = "Attacking";
    const string TAG_PLAYER = "Player";

    //Components
    PlayerController playerController;
    MovementCharacter myMovement;


    void Start() 
    {
        player = GameObject.FindWithTag(TAG_PLAYER);
        int generateTypeZombie = Random.Range(1, 28);
        transform.GetChild(generateTypeZombie).gameObject.SetActive(true);
        playerController = player.GetComponent<PlayerController>(); 
        myMovement = GetComponent<MovementCharacter>(); 
    }

    void FixedUpdate()
    {
        float distanceBetweenZombiAndPlayer = Vector3.Distance(
            transform.position, player.transform.position
        );

        //Zombi Rotation
        Vector3 direction = player.transform.position - transform.position;
        myMovement.Rotation(direction);

        bool attacking = true;

        if (distanceBetweenZombiAndPlayer > 2.5) {
            myMovement.Movement(direction.normalized, ZombiSpeed);
            attacking = false;
        } 
        
        GetComponent<Animator>().SetBool(ANIMATOR_ATTACKING, attacking);
    }

    void AttackPlayer() 
    {
        playerController.takeDamage(DamageCaused);
    }
}
