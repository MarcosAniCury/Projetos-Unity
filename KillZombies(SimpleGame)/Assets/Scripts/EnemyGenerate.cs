using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    //Public vars
    public GameObject Zombie;
    public float GenerateEnemyTime = 1;

    //Private vars
    private float timeCount;

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount > GenerateEnemyTime) {
            Instantiate(Zombie, transform.position, transform.rotation);
            timeCount = 0;
        }
    }
}
