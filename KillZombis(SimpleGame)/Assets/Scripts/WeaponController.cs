using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public GameObject bullet;
    public GameObject barrelGun;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Instantiate(bullet, barrelGun.transform.position, barrelGun.transform.rotation);
        }
    }
}
