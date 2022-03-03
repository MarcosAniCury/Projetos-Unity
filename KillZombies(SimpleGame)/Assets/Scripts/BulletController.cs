using UnityEngine;

public class BulletController : MonoBehaviour
{
    //public vars
    public float BulletSpeed = 20;

    //CONSTs
    const int DAMAGE_IN_ZOMBIE = 1;

    //Components
    Rigidbody bulletRigidbody;

    void Start() 
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() 
    {
        bulletRigidbody.MovePosition(
            bulletRigidbody.position + (
                transform.forward *
                BulletSpeed *
                Time.deltaTime
            )
        );
    }

    void OnTriggerEnter(Collider colliderObject) 
    {
        if (colliderObject.tag == Constants.TAG_ENEMY) {
            colliderObject.GetComponent<ZombieController>().TakeDamage(DAMAGE_IN_ZOMBIE);
        }

        Destroy(gameObject);
    }
}
