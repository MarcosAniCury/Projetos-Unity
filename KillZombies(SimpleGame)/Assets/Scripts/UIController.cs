using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //Public vars
    public Slider LifePlayerSlider;
    
    //Private vars
    PlayerController playerController;

    void Start()
    {
        playerController = GameObject.
            FindWithTag(Constants.TAG_PLAYER).
            GetComponent<PlayerController>();
        LifePlayerSlider.maxValue = playerController.myStatus.Life;
        LifePlayerSlider.value = playerController.myStatus.Life;
    }

    public void updatedLivePlayerSlider()
    {
        LifePlayerSlider.value = playerController.myStatus.Life;
    }
}
