using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    //Public vars
    public GameObject Zombie;
    public float generateEnemyTime = 1;

    //Private vars
    float timeCount;

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount > generateEnemyTime) {
            Instantiate(Zombie, transform.position, transform.rotation);
            timeCount = 0;
        }
    }
}
