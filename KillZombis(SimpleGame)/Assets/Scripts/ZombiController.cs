using UnityEngine;

public class ZombiController : MonoBehaviour
{
    public GameObject player;
    public float zombiSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
