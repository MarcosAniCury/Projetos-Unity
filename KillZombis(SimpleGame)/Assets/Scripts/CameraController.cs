using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public vars
    public GameObject player;

    //private vars
    Vector3 distanceBetweenPlayerAndCamera;

    void Start() {
        distanceBetweenPlayerAndCamera = transform.position - player.transform.position;
    }

    void Update() {
        transform.position = player.transform.position + distanceBetweenPlayerAndCamera;
    }
}
