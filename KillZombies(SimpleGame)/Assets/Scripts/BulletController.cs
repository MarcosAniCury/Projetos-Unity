using UnityEngine;

public class BulletController : MonoBehaviour
{
    //public vars
    public float BulletSpeed = 20;
    public AudioClip ZombieDieSound;

    //CONSTs
    const string TAG_ENEMY = "Enemy";

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
        if (colliderObject.tag == TAG_ENEMY) {
            Destroy(colliderObject.gameObject);
            SoundController.instance.PlayOneShot(ZombieDieSound);
        }

        Destroy(gameObject);
    }
}
