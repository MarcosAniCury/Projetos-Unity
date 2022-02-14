using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    Vector3 distanceBetweenPlayerAndCamera;

    void Start() {
        distanceBetweenPlayerAndCamera = transform.position - player.transform.position;
    }

    void Update() {
        transform.position = player.transform.position + distanceBetweenPlayerAndCamera;
    }
}
