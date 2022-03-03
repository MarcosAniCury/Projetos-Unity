using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //Public vars
    public Slider LifePlayerSlider;
    
    //Private vars
    PlayerController playerController;

    //CONSTs
    const string TAG_PLAYER = "Player";

    void Start()
    {
        playerController = GameObject.
            FindWithTag(TAG_PLAYER).
            GetComponent<PlayerController>();
        LifePlayerSlider.maxValue = playerController.myStatus.Life;
        LifePlayerSlider.value = playerController.myStatus.Life;
    }

    public void updatedLivePlayerSlider()
    {
        LifePlayerSlider.value = playerController.myStatus.Life;
    }
}
