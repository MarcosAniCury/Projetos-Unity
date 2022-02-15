using UnityEngine;

public class ZombiController : MonoBehaviour
{
    //Publics vars
    public GameObject player;
    public float zombiSpeed = 5;

    //CONSTs
    const string ANIMATOR_ATTACKING = "Attacking";

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
                ( direction.normalized * zombiSpeed * Time.deltaTime )
            );
            GetComponent<Animator>().SetBool(ANIMATOR_ATTACKING, false);
        } else {
            GetComponent<Animator>().SetBool(ANIMATOR_ATTACKING, true);
        }
    }
}
