using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float bulletSpeed = 20;

    void FixedUpdate() {
        GetComponent<Rigidbody>().MovePosition(
            GetComponent<Rigidbody>().position + (
                transform.forward *
                bulletSpeed *
                Time.deltaTime
            )
        );
    }
}
