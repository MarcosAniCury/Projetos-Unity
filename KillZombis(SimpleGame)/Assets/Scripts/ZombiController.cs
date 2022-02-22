using UnityEngine;

public class ZombiController : MonoBehaviour
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

    Rigidbody zombiRigidbody;

    PlayerController playerController;


    void Start() 
    {
        player = GameObject.FindWithTag(TAG_PLAYER);
        int generateTypeZombie = Random.Range(1, 28);
        transform.GetChild(generateTypeZombie).gameObject.SetActive(true);
        zombiRigidbody = GetComponent<Rigidbody>();
        playerController = player.GetComponent<PlayerController>();  
    }

    void FixedUpdate()
    {
        float distanceBetweenZombiAndPlayer = Vector3.Distance(
            transform.position, player.transform.position
        );

        //Zombi Rotation
        Vector3 direction = player.transform.position - transform.position;
        Quaternion zombiDirection = Quaternion.LookRotation(direction);
        zombiRigidbody.MoveRotation(zombiDirection);

        bool attacking = true;

        if (distanceBetweenZombiAndPlayer > 2.5) {
            //Zombi move
            zombiRigidbody.MovePosition(
                zombiRigidbody.position + 
                ( direction.normalized * ZombiSpeed * Time.deltaTime )
            );

            attacking = false;
        } 
        
        GetComponent<Animator>().SetBool(ANIMATOR_ATTACKING, attacking);
    }

    void AttackPlayer() 
    {
        playerController.takeDamage(DamageCaused);
    }
}
