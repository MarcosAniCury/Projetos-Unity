using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float bulletSpeed = 20;

    const string TAG_ENEMY = "Enemy";

    void FixedUpdate() 
    {
        GetComponent<Rigidbody>().MovePosition(
            GetComponent<Rigidbody>().position + (
                transform.forward *
                bulletSpeed *
                Time.deltaTime
            )
        );
    }

    void OnTriggerEnter(Collider colliderObject) 
    {
        if (colliderObject.tag == TAG_ENEMY) {
            Destroy(colliderObject.gameObject);
        }

        Destroy(gameObject);
    }
}
