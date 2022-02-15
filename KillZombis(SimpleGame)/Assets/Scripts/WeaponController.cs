using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //public vars
    public GameObject bullet;
    public GameObject barrelGun;

    //CONSTs
    const string INPUT_MOUSE_LEFT = "Fire1";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(INPUT_MOUSE_LEFT)) {
            Instantiate(bullet, barrelGun.transform.position, barrelGun.transform.rotation);
        }
    }
}
