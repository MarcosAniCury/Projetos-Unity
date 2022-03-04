using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    //CONSTs
    const int AMOUNT_HEAL = 15;
    const int TIME_SELF_DESTROY = 5;

    void Start()
    {
        Destroy(gameObject, TIME_SELF_DESTROY);
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if(otherCollider.tag == Constants.TAG_PLAYER) {
            otherCollider.GetComponent<PlayerController>().Healing(AMOUNT_HEAL);
            Destroy(gameObject);
        }
    }
}
