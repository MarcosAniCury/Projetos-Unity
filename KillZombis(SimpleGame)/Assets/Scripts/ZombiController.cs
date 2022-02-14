using UnityEngine;

public class ZombiController : MonoBehaviour
{
    public GameObject player;
    public float zombiSpeed = 5;

    void FixedUpdate()
    {
        float distanceBetweenZombiAndPlayer = Vector3.Distance(
            transform.position, player.transform.position
        );

        if (distanceBetweenZombiAndPlayer > 2.5) {

            //Zombi move
            Vector3 direction = player.transform.position - transform.position;
            GetComponent<Rigidbody>().MovePosition(
                GetComponent<Rigidbody>().position + 
                ( direction.normalized * zombiSpeed * Time.deltaTime )
            );

            //Zombi Rotation
            Quaternion zombiDirection = Quaternion.LookRotation(direction);
            GetComponent<Rigidbody>().MoveRotation(zombiDirection);
        }
    }
}
