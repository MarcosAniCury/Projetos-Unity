using UnityEngine;

public class ZombiController : MonoBehaviour
{
    //Public vars
    public float ZombiSpeed = 5;

    //Private vars
    private GameObject player;

    //CONSTs
    const string ANIMATOR_ATTACKING = "Attacking";
    const int GAME_PAUSE = 0;
    const string TAG_PLAYER = "Player";

    void Start() 
    {
        player = GameObject.FindWithTag(TAG_PLAYER);
        int generateTypeZombie = Random.Range(1, 28);
        transform.GetChild(generateTypeZombie).gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        float distanceBetweenZombiAndPlayer = Vector3.Distance(
            transform.position, player.transform.position
        );

        //Zombi Rotation
        Vector3 direction = player.transform.position - transform.position;
        Quaternion zombiDirection = Quaternion.LookRotation(direction);
        GetComponent<Rigidbody>().MoveRotation(zombiDirection);

        if (distanceBetweenZombiAndPlayer > 2.5) {
            //Zombi move
            GetComponent<Rigidbody>().MovePosition(
                GetComponent<Rigidbody>().position + 
                ( direction.normalized * ZombiSpeed * Time.deltaTime )
            );

            GetComponent<Animator>().SetBool(ANIMATOR_ATTACKING, false);
        } else {
            GetComponent<Animator>().SetBool(ANIMATOR_ATTACKING, true);
        }
    }

    void AttackPlayer() 
    {
        Time.timeScale = GAME_PAUSE;
        player.GetComponent<PlayerController>().GameOverCanvas.SetActive(true);
        player.GetComponent<PlayerController>().GameOver = true;
    }
}
